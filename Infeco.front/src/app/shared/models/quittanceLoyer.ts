export class IQuittanceLoyer {
  id: number;
  mois: number;
  annee: number;
  montant: number;
  locataireAppartementId: number;
}

export class QuittanceLoyer implements IQuittanceLoyer {
  id: number;
  mois: number;
  annee: number;
  montant: number;
  locataireAppartementId: number;

  deserialize(input: IQuittanceLoyer): QuittanceLoyer {
    if (input) {
      const quittance = new QuittanceLoyer();
      quittance.id = input.id;
      quittance.mois = input.mois;
      quittance.annee = input.annee;
      quittance.montant = input.montant;
      quittance.locataireAppartementId = input.locataireAppartementId;

      return quittance;
    }
    return null as any;
  }

  deserializeList(input: IQuittanceLoyer[]): QuittanceLoyer[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IQuittanceLoyer {
    return {
      id: this.id,
      locataireAppartementId: this.locataireAppartementId,
      mois: this.mois,
      annee: this.annee,
      montant: this.montant,
    };
  }
}
