export class IEtatDesLieux {
  id: number;
  dateEtatDesLieux: Date;
  remarque: string;
  locataireAppartementId: number;
}

export class EtatDesLieux implements IEtatDesLieux {
  id: number;
  dateEtatDesLieux: Date;
  remarque: string;
  locataireAppartementId: number;

  deserialize(input: IEtatDesLieux): EtatDesLieux {
    if (input) {
      const etatDesLieux = new EtatDesLieux();
      etatDesLieux.id = input.id;
      etatDesLieux.locataireAppartementId = input.locataireAppartementId;
      etatDesLieux.dateEtatDesLieux = input.dateEtatDesLieux;
      etatDesLieux.remarque = input.remarque;
      return etatDesLieux;
    }
    return null as any;
  }

  deserializeList(input: IEtatDesLieux[]): EtatDesLieux[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IEtatDesLieux {
    return {
      id: this.id,
      locataireAppartementId: this.locataireAppartementId,
      remarque: this.remarque,
      dateEtatDesLieux: this.dateEtatDesLieux,
    };
  }
}

export class AjoutEtatDesLieux {
  locataireAppartementId: number;
  dateEtatDesLieux: Date;
  remarque: string;
}

export class EtatDesLieuxAjoutRetour {
  id: number;
}
