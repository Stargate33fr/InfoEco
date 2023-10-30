import { Appartement, IAppartement } from './appartement';
import { ILocataire, Locataire } from './locataire';

export interface ILocataireAppartement {
  id: number;
  appartementId: number;
  depotDeGarantieRegle: boolean;
  locataires: ILocataire[];
  appartement: IAppartement;
}

export class LocataireAppartement implements ILocataireAppartement {
  id: number;
  appartementId: number;
  depotDeGarantieRegle: boolean;
  locataires: ILocataire[];
  appartement: IAppartement;

  deserialize(input: ILocataireAppartement): LocataireAppartement {
    if (input) {
      const locataireAppartement = new LocataireAppartement();
      locataireAppartement.id = input.id;
      locataireAppartement.appartementId = input.appartementId;
      locataireAppartement.depotDeGarantieRegle = input.depotDeGarantieRegle;
      locataireAppartement.locataires = new Locataire().deserializeList(input.locataires);
      locataireAppartement.appartement = new Appartement().deserialize(input.appartement);

      return locataireAppartement;
    }
    return null as any;
  }

  deserializeList(input: ILocataireAppartement[]): LocataireAppartement[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): ILocataireAppartement {
    return {
      id: this.id,
      appartementId: this.appartementId,
      depotDeGarantieRegle: this.depotDeGarantieRegle,
      locataires: this.locataires,
      appartement: this.appartement,
    };
  }
}

export class DetailAppartement {
  id: number;
  locataireAppartement: ILocataireAppartement;
}

export class AjoutLocataire {
  civiliteId: number;
  nom: string;
  prenom: string;
  mail: string;
  dateDeNaissance: Date;
  telephone: string;
  iban: string;
}

export class AssignerLocataire {
  appartementId: number;
  locataireId: number;
  depotDeGarantieRegle: boolean;
  dateEntree: Date;
}

export class LocataireAppartementAjoutRetour {
  id: number;
}
