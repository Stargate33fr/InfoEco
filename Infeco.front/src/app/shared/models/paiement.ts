import { ILocataire, Locataire } from './locataire';
import { ISelect, Select } from './select';

export interface IPaiement {
  id: number;
  locataireAppartementId: number;
  montant: number;
  typePaiementId: number;
  datePaiement: Date;
  locataire: ILocataire;
  typePaiement: ISelect;
}

export class Paiement implements IPaiement {
  id: number;
  locataireAppartementId: number;
  montant: number;
  typePaiementId: number;
  datePaiement: Date;
  typePaiement: ISelect;
  locataire: ILocataire;

  deserialize(input: IPaiement): Paiement {
    if (input) {
      const paiement = new Paiement();
      paiement.id = input.id;
      paiement.locataireAppartementId = input.locataireAppartementId;
      paiement.montant = input.montant;
      paiement.typePaiementId = input.typePaiementId;
      paiement.datePaiement = new Date(input.datePaiement);
      paiement.typePaiement = new Select().deserialize(input.typePaiement);
      paiement.locataire = new Locataire().deserialize(input.locataire);

      return paiement;
    }
    return null as any;
  }

  deserializeList(input: IPaiement[]): Paiement[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IPaiement {
    return {
      id: this.id,
      locataireAppartementId: this.locataireAppartementId,
      montant: this.montant,
      typePaiementId: this.typePaiementId,
      datePaiement: this.datePaiement,
      typePaiement: this.typePaiement,
      locataire: this.locataire,
    };
  }
}

export class LocatairePaiementAjout {
  locataireAppartementId: number;
  montant: number;
  typePaiementId: number;
  datePaiement: Date;
  genererQuittanceLoyer: boolean;
  mois: number;
  annee: number;
}

export class LocatairePaiementRetour {
  id: number;
}
