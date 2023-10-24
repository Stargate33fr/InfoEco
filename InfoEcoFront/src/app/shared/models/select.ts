export enum ChoixOuiNon {
  oui = 1,
  non = 0,
}

export interface ISelect {
  id: number;
  nom: string;
  sousListe?: ISelect[];
  estVisible: boolean;
  valeurAssociee: string;
  codePostal: string;
}

export class Select implements ISelect {
  id: number;
  nom: string;
  sousListe?: ISelect[] = [];
  estVisible: boolean;
  valeurAssociee: string;
  codePostal: string;

  deserialize(input: ISelect): Select {
    if (input) {
      const selection = new Select();
      selection.id = input.id;
      selection.estVisible = input.estVisible;
      selection.nom = input.nom;
      selection.codePostal = input.codePostal;
      selection.sousListe = input.sousListe ? new Select().deserializeList(input.sousListe) : [];
      selection.valeurAssociee = input.valeurAssociee;

      return selection;
    }
    return null as any;
  }

  deserializeList(input: ISelect[]): Select[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): ISelect {
    return {
      id: this.id,
      nom: this.nom,
      codePostal: this.codePostal,
      sousListe: this.sousListe,
      valeurAssociee: this.valeurAssociee,
      estVisible: this.estVisible,
    };
  }
}
