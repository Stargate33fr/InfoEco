using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MimeKit;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GenereAndSendPdf
{
    public class Locataire
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
    }

    public class Appartement
    { 
        public int AppartementId { get; set; }
        public string Residence { get; set; }
        public string adresse { get; set; }
    }

    public class Paiement
    {
        public DateTime DatePaiement { get; set; }
        public decimal Montant { get; set; }
        // Autres détails du paiement
    }


    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task Run([TimerTrigger("0 0 1 1 * *")] TimerInfo myTimer, ILogger log) // Déclenchement toutes les jours à 8 heures
        {
            string connectionString = "Host=snuffleupagus.db.elephantsql.com;Username=pmjmvgha;Password=ktvzW58l-VY2c6fwNbff1zndMiy3qzuJ;Database=pmjmvgha";
            var targetDate = GetTargetDateFromPostgreSQL(connectionString);
            List<Appartement> apartments = new List<Appartement>();
            if (targetDate.Date.Month == DateTime.Today.Month && targetDate.Date.Day == DateTime.Today.Day)
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("select * from \"Appartement\"", connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int appartementId = int.Parse(reader["Id"].ToString());
                            string apartmentAdresse = reader["Adresse"].ToString(); // Remplacez par le nom de votre colonne d'appartement
                            string aappartementResidenceAdresse = reader["NomResidence"].ToString();
                            var appartement = new Appartement();
                            appartement.adresse = apartmentAdresse;
                            appartement.Residence = aappartementResidenceAdresse;
                            appartement.AppartementId = appartementId;
                            // Récupération des données spécifiques à l'appartement depuis la base de données
                            apartments.Add(appartement);
                        }
                    }
                }
                foreach (var apartmentName in apartments)
                {
                    string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=bilansappartementsloyer;AccountKey=59dHAvPi+jEFDqGLWWfmdgkejvK8/Mik8a7+FoZ0Jv/NlUZxtbN0nKymvTghPJAxpnYsjtxytU2m+AStT/LKsQ==;EndpointSuffix=core.windows.net";
                    string containerName = "container-name";
                    string blobName = $"clients_list_appartement_${apartmentName}.pdf";
                    List<Locataire> locataires = GetLocatairesForApartment(apartmentName.AppartementId, connectionString);
                    List<Paiement> paiements = GetPaiementsForApartment(apartmentName.AppartementId, connectionString);
                    await GeneratePDFPerApartmentAsync(locataires, paiements, apartmentName.AppartementId.ToString(), log);
                    await SendEmailWithAttachment(storageConnectionString, containerName, blobName, log);
                }
            }
            else
            {
                log.LogInformation($"La date cible n'est pas encore atteinte : {DateTime.Today}");
            }
        }

        public static async Task GeneratePDFPerApartmentAsync(List<Locataire> locataires, List<Paiement> paiements, string apartmentName, ILogger log)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=bilansappartementsloyer;AccountKey=59dHAvPi+jEFDqGLWWfmdgkejvK8/Mik8a7+FoZ0Jv/NlUZxtbN0nKymvTghPJAxpnYsjtxytU2m+AStT/LKsQ==;EndpointSuffix=core.windows.net";
            string containerName = "container-name";
            string blobName = $"clients_list_appartement_${apartmentName}.pdf";

            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.Add(new Paragraph($"Détails de l'appartement {apartmentName}"));

                document.Add(new Paragraph("Locataires :"));
                foreach (var locataire in locataires)
                {
                  document.Add(new Paragraph($"Nom : {locataire.Nom}, Prénom : {locataire.Prenom}"));
                }
                // Informations sur les paiements
                document.Add(new Paragraph("Paiements :"));
                foreach (var paiement in paiements)
                {
                    document.Add(new Paragraph($"Date paiement : {paiement.DatePaiement}, Montant : {paiement.Montant}"));
                }

                document.Close();
                writer.Close();
                
                await UploadFileToBlob(ms.ToArray(), storageConnectionString, containerName, blobName, log);
            }
        }

        public static List<Locataire> GetLocatairesForApartment(int appartementId, string connectionString)
        {
            // Récupération des locataires spécifiques à l'appartement depuis la base de données
            // Exemple de requête :
            var locataires = new List<Locataire>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command2 = new NpgsqlCommand($"select l.\"Nom\" , l.\"Prenom\" , l.\"Telephone\" , l.\"Mail\"  from \"LocataireAppartement\" la \r\ninner join \"Appartement\" a on a.\"Id\" = la.\"AppartementId\" \r\ninner join \"Locataire\" l on l.\"Id\"  = la.\"LocataireId\" where la.\"AppartementId\" = {appartementId}", connection))
                using (var reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locataires.Add(new Locataire
                        {
                            Nom = reader["Nom"].ToString(),
                            Prenom = reader["Prenom"].ToString(),
                            Telephone = reader["Telephone"].ToString(),
                            Mail = reader["Mail"].ToString(),
                            // Récupérez d'autres colonnes de la table si nécessaire
                        });
                    }
                }
            }
            return locataires;
        }

        public static List<Paiement> GetPaiementsForApartment(int appartementId, string connectionString)
        {
            // Récupération des paiements spécifiques à l'appartement depuis la base de données
            // Exemple de requête :
            var paiements = new List<Paiement>();
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand($"select p.\"Montant\" , p.\"DatePaiement\" from \"LocataireAppartement\" la inner join \"Appartement\" a on a.\"Id\" = la.\"AppartementId\" \r\ninner join \"Locataire\" l on l.\"Id\"  = la.\"LocataireId\" inner join \"Paiement\" p on p.\"LocataireAppartementId\" = la.\"Id\" inner join \"TypePaiement\" tp on tp.\"Id\" = p.\"TypePaiementId\" where la.\"AppartementId\"   ={appartementId}  and tp.\"Nom\" ='Loyer'\r\ngroup by p.\"Montant\" , p.\"DatePaiement\"", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paiements.Add(new Paiement
                        {
                            DatePaiement = Convert.ToDateTime(reader["DatePaiement"]),
                            Montant = Convert.ToDecimal(reader["Montant"]),
                            // Récupérez d'autres colonnes de la table si nécessaire
                        });
                    }
                }
            }
            return paiements;
        }

        private static DateTime GetTargetDateFromPostgreSQL(string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT \"Date\" FROM public.\"Bilan\" limit 1", connection))
                {
                    var result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        // Si votre colonne de date est de type DateTime dans PostgreSQL
                        return Convert.ToDateTime(result);
                    }
                }
            }

            // Retourner une date par défaut si aucune date n'est trouvée dans la base de données
            return DateTime.MinValue;
        }

        public static async Task UploadFileToBlob(byte[] fileBytes, string storageConnectionString, string containerName, string blobName, ILogger log)
        {
            try
            {
                // Créer une référence à un conteneur de stockage
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Créer le conteneur s'il n'existe pas
                await container.CreateIfNotExistsAsync();

                // Obtenir une référence à un bloc blob
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

                // Charger le fichier PDF dans le blob
                using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                {
                    await blockBlob.UploadFromStreamAsync(memoryStream);
                }

                log.LogInformation($"Fichier {blobName} téléversé avec succès dans le blob storage.");
            }
            catch (Exception ex)
            {
                log.LogError("Erreur lors de l'envoi du fichier vers le stockage Blob : " + ex.Message);
                throw;
            }
            finally
            {
              
            }
        }

        public static async Task SendEmailWithAttachment(string storageConnectionString, string containerName, string blobName, ILogger log)
        {
            try
            {
                // Pièce jointe du fichier PDF
                var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(containerName);
                var blockBlob = container.GetBlockBlobReference(blobName);

                var fileStream = new MemoryStream();
                await blockBlob.DownloadToStreamAsync(fileStream);
                fileStream.Position = 0;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender Name", "sender@example.com")); // Remplacez par l'adresse expéditeur
                message.To.Add(new MailboxAddress("Recipient Name", "recipient@example.com")); // Remplacez par l'adresse destinataire
                message.Subject = "Objet de l'e-mail";

                var builder = new BodyBuilder();

                // Ajouter la pièce jointe
                builder.Attachments.Add("clients_list.pdf", fileStream);

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.example.com", 587, SecureSocketOptions.StartTls); // Remplacez par les détails du serveur SMTP
                    await client.AuthenticateAsync("your_email@example.com", "your_password"); // Remplacez par les informations d'authentification SMTP

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                log.LogInformation("E-mail envoyé avec succès.");
            }
            catch (Exception ex)
            {
                log.LogError("Erreur lors de l'envoi de l'e-mail : " + ex.Message);
                throw;
            }
        }
    }
}
