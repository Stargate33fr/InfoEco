export interface IVille {
  codePostal: string;
  id: number;
  nom: string;
}

export interface IProvenancePaiement {
  id: number;
  nom: string;
}

export interface ITypeAppartement {
  id: number;
  nom: string;
}

export class TypeAppartement implements ITypeAppartement {
  id: number;
  nom: string;

  deserialize(input: ITypeAppartement): TypeAppartement {
    if (input) {
      const typeAppartement = new TypeAppartement();
      typeAppartement.id = input.id;
      typeAppartement.nom = input.nom;

      return typeAppartement;
    }
    return null as any;
  }

  deserializeList(input: ITypeAppartement[]): TypeAppartement[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): ITypeAppartement {
    return {
      id: this.id,
      nom: this.nom,
    };
  }
}

export class ProvenancePaiement implements IProvenancePaiement {
  id: number;
  nom: string;

  deserialize(input: IProvenancePaiement): ProvenancePaiement {
    if (input) {
      const provenancePaiement = new ProvenancePaiement();
      provenancePaiement.id = input.id;
      provenancePaiement.nom = input.nom;

      return provenancePaiement;
    }
    return null as any;
  }

  deserializeList(input: IProvenancePaiement[]): ProvenancePaiement[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IProvenancePaiement {
    return {
      id: this.id,
      nom: this.nom,
    };
  }
}

export class Ville implements IVille {
  codePostal: string;
  id: number;
  nom: string;

  deserialize(input: IVille): Ville {
    if (input) {
      const ville = new Ville();
      ville.codePostal = input.codePostal;
      ville.id = input.id;
      ville.nom = input.nom;

      return ville;
    }
    return null as any;
  }

  deserializeList(input: IVille[]): Ville[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IVille {
    return {
      codePostal: this.codePostal,
      id: this.id,
      nom: this.nom,
    };
  }
}
