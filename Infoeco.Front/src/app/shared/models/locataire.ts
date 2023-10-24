export interface ILocataire {
  id: number;
  nom: string;
  prenom: string;
  mail: string;
  telephone: string;
  dateNaissance: Date;
  iban: string;
}

export class Locataire implements ILocataire {
  id: number;
  nom: string;
  prenom: string;
  mail: string;
  telephone: string;
  dateNaissance: Date;
  iban: string;

  deserialize(input: ILocataire): Locataire {
    if (input) {
      const locataire = new Locataire();
      locataire.id = input.id;
      locataire.nom = input.nom;
      locataire.prenom = input.prenom;
      locataire.mail = input.mail;
      locataire.telephone = input.telephone;
      locataire.dateNaissance = input.dateNaissance;
      locataire.iban = input.iban;

      return locataire;
    }
    return null as any;
  }

  deserializeList(input: ILocataire[]): Locataire[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): ILocataire {
    return {
      id: this.id,
      nom: this.nom,
      prenom: this.prenom,
      mail: this.mail,
      telephone: this.telephone,
      dateNaissance: this.dateNaissance,
      iban: this.iban,
    };
  }
}
