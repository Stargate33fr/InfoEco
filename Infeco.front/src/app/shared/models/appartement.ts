import { IDeserializable } from '@core/model/deserializable.interface';
import { ITypeAppartement, IVille, TypeAppartement, Ville } from './donneeReference';
import { ResponseAPIDto } from '@core/dto/response-api';

export interface IAppartement {
  id: number;
  adresse: string;
  complementAdresse: string;
  ville: IVille;
  typeAppartement: ITypeAppartement;
  prixDesCharges: number;
  loyer: number;
  depotDeGarantie: number;
  nombreDeMetre2: number;
  agenceImmobiliereId: number;
  nomResidence: string;
  numeroAppartement: number;
}

export class Appartement implements IAppartement {
  id: number;
  adresse: string;
  complementAdresse: string;
  ville: Ville;
  typeAppartement: TypeAppartement;
  prixDesCharges: number;
  loyer: number;
  depotDeGarantie: number;
  nombreDeMetre2: number;
  agenceImmobiliereId: number;
  nomResidence: string;
  numeroAppartement: number;

  deserialize(input: IAppartement): Appartement {
    if (input) {
      const appartement = new Appartement();
      appartement.id = input.id;
      appartement.adresse = input.adresse;
      appartement.complementAdresse = input.complementAdresse;
      appartement.ville = new Ville().deserialize(input.ville);
      appartement.typeAppartement = new TypeAppartement().deserialize(input.typeAppartement);
      appartement.loyer = input.loyer;
      appartement.prixDesCharges = input.prixDesCharges;
      appartement.depotDeGarantie = input.depotDeGarantie;
      appartement.nombreDeMetre2 = input.nombreDeMetre2;
      appartement.agenceImmobiliereId = input.agenceImmobiliereId;
      appartement.nomResidence = input.nomResidence;
      appartement.numeroAppartement = input.numeroAppartement;
      return appartement;
    }
    return null as any;
  }

  deserializeList(input: IAppartement[]): Appartement[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IAppartement {
    return {
      id: this.id,
      adresse: this.adresse,
      complementAdresse: this.complementAdresse,
      ville: this.ville,
      loyer: this.loyer,
      depotDeGarantie: this.depotDeGarantie,
      nombreDeMetre2: this.nombreDeMetre2,
      agenceImmobiliereId: this.agenceImmobiliereId,
      typeAppartement: this.typeAppartement,
      prixDesCharges: this.prixDesCharges,
      nomResidence: this.nomResidence,
      numeroAppartement: this.numeroAppartement,
    };
  }
}

export class Appartements implements IDeserializable<Appartements> {
  valeur: Appartement[];
  total: number;

  deserialize(input: ResponseAPIDto<IAppartement[]>): Appartements {
    if (input) {
      const appartements = new Appartements();
      appartements.valeur = input.contenu.map((item) => new Appartement().deserialize(item));
      appartements.total = input.total ?? 0;

      return appartements;
    }
    return new Appartements();
  }
}

export class FilterAppartement {
  agenceImmobiliereId: number;
  nomVille: string;
  adresse: string;
  nomLocataire: string;
  typeAppartementId: number;
}

export class FilterLocataireAppartement {
  id: number;
  locataireId: number;
  nom: string;
  email: string;
  appartementId: number;
  villeId: number;
}

export class AppartementAjoutRetour {
  id: number;
}

export class AjoutModifierAppartement {
  adresse: string;
  complementAdresse: string;
  villeId: number;
  prixDesCharges: number;
  loyer: number;
  depotDeGarantie: number;
  nombreDeMetre2: number;
  typeAppartementId: number;
  nomResidence: string;
  numeroAppartement: number;
}
