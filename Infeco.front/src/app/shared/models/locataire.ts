import { ISelect, Select } from './select';

export interface ILocataire {
  id: number;
  nom: string;
  prenom: string;
  mail: string;
  telephone: string;
  dateNaissance: Date;
  iban: string;
  civiliteId: number;
  dateEntree: Date;
  dateSortie: Date;
  civilite: ISelect;
}

export class Locataire implements ILocataire {
  id: number;
  civiliteId: number;
  nom: string;
  prenom: string;
  mail: string;
  telephone: string;
  dateNaissance: Date;
  iban: string;
  dateEntree: Date;
  dateSortie: Date;
  civilite: ISelect;

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
      locataire.dateEntree = input.dateEntree;
      locataire.dateSortie = input.dateSortie;
      locataire.civiliteId = input.civiliteId;
      locataire.civilite = new Select().deserialize(input.civilite);

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
      civiliteId: this.civiliteId,
      id: this.id,
      nom: this.nom,
      prenom: this.prenom,
      mail: this.mail,
      telephone: this.telephone,
      dateNaissance: this.dateNaissance,
      iban: this.iban,
      dateEntree: this.dateEntree,
      dateSortie: this.dateSortie,
      civilite: this.civilite,
    };
  }
}

export class AjoutModifierLocataire {
  civiliteId: number;
  nom: string;
  prenom: string;
  mail: string;
  telephone: string;
  dateNaissance: Date;
  iban: string;
}

export class LocataireAjoutRetour {
  id: number;
}
