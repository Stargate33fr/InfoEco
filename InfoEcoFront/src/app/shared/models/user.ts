export interface IUser {
  nom: string;
  prenom: string;
  id: number;
  courriel: string;
  telephone: string;
  mobile: string;
  agenceImmobiliereId: number;
}

export interface IUsers {
  contenu: IUser[];
  total?: number;
}

export interface ISociete {
  id: number;
  raisonSociale: string;
  description: string;
  adresseId: number;
}

export class Users implements IUsers {
  contenu: IUser[];
  total?: number;

  deserialize(input: IUsers): Users {
    if (input) {
      const users = new Users();
      users.contenu = new User().deserializeList(input.contenu);
      users.total = input.total;

      return users;
    }
    return null as any;
  }

  deserializeList(input: IUsers[]): Users[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IUsers {
    return {
      contenu: this.contenu,
      total: this.total,
    };
  }
}

export class Societe implements ISociete {
  id: number;
  raisonSociale: string;
  description: string;
  adresseId: number;

  deserialize(input: ISociete): Societe {
    if (input) {
      const societe = new Societe();
      societe.id = input.id;
      societe.raisonSociale = input.raisonSociale;
      societe.description = input.description;
      societe.adresseId = input.adresseId;

      return societe;
    }
    return null as any;
  }

  deserializeList(input: ISociete[]): Societe[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): ISociete {
    return {
      id: this.id,
      raisonSociale: this.raisonSociale,
      description: this.description,
      adresseId: this.adresseId,
    };
  }
}

export class User implements IUser {
  nom: string;
  prenom: string;
  id: number;
  courriel: string;
  telephone: string;
  mobile: string;
  agenceImmobiliereId: number;

  deserialize(input: IUser): User {
    if (input) {
      const user = new User();
      user.nom = input.nom;
      user.prenom = input.prenom;
      user.id = input.id;
      user.courriel = input.courriel;
      user.telephone = input.telephone;
      user.mobile = input.mobile;
      user.agenceImmobiliereId = input.agenceImmobiliereId;

      return user;
    }
    return null as any;
  }

  deserializeList(input: IUser[]): User[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IUser {
    return {
      nom: this.nom,
      prenom: this.prenom,
      id: this.id,
      courriel: this.courriel,
      telephone: this.telephone,
      mobile: this.mobile,
      agenceImmobiliereId: this.agenceImmobiliereId,
    };
  }
}

export interface IUpdateUser {
  nom: string;
  prenom: string;
  telephone: string;
  mobile: string;
  dateDerniereConnexion: Date;
}

export class UpdateUser implements IUpdateUser {
  nom: string;
  prenom: string;
  telephone: string;
  mobile: string;
  dateDerniereConnexion: Date;

  deserialize(input: IUpdateUser): UpdateUser {
    if (input) {
      const updateUser = new UpdateUser();
      updateUser.nom = input.nom;
      updateUser.prenom = input.prenom;
      updateUser.telephone = input.telephone;
      updateUser.mobile = input.mobile;
      updateUser.dateDerniereConnexion = input.dateDerniereConnexion;

      return updateUser;
    }
    return null as any;
  }

  deserializeList(input: IUpdateUser[]): UpdateUser[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IUpdateUser {
    return {
      nom: this.nom,
      prenom: this.prenom,
      telephone: this.telephone,
      mobile: this.mobile,
      dateDerniereConnexion: this.dateDerniereConnexion,
    };
  }
}
