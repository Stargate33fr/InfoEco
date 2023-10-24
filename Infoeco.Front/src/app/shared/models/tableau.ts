import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';
import { ILocataire, Locataire } from './locataire';

export interface IDataItem {
  checked: boolean;
  adresse: string;
  complementAdresse: string;
  depotDeGarantie: number;
  id: number;
  loyer: number;
  nombreDeMetre2: number;
  typeAppartement: string;
  ville: string;
  locataires: ILocataire[];
  nomResidence: string;
  numeroAppartement: number;
}

export interface IColumnItem {
  name: string;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn<DataItem> | null;
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn<DataItem> | null;
  filterMultiple: boolean;
  sortDirections: NzTableSortOrder[];
  width: string;
  left: boolean;
  right: boolean;
}

export class DataItem implements IDataItem {
  checked: boolean;
  adresse: string;
  complementAdresse: string;
  depotDeGarantie: number;
  id: number;
  loyer: number;
  nombreDeMetre2: number;
  typeAppartement: string;
  codePostal: string;
  ville: string;
  locataires: ILocataire[];
  nomResidence: string;
  numeroAppartement: number;

  deserialize(input: IDataItem): DataItem {
    if (input) {
      const dataItem = new DataItem();
      dataItem.checked = input.checked;
      dataItem.adresse = input.adresse;
      dataItem.complementAdresse = input.complementAdresse;
      dataItem.depotDeGarantie = input.depotDeGarantie;
      dataItem.id = input.id;
      dataItem.loyer = input.loyer;
      dataItem.nombreDeMetre2 = input.nombreDeMetre2;
      dataItem.typeAppartement = input.typeAppartement;
      dataItem.ville = input.ville;
      dataItem.numeroAppartement = input.numeroAppartement;
      dataItem.nomResidence = input.nomResidence;
      dataItem.locataires = new Locataire().deserializeList(input.locataires);

      return dataItem;
    }
    return null as any;
  }

  deserializeList(input: IDataItem[]): DataItem[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IDataItem {
    return {
      checked: this.checked,
      adresse: this.adresse,
      complementAdresse: this.complementAdresse,
      depotDeGarantie: this.depotDeGarantie,
      id: this.id,
      loyer: this.loyer,
      nombreDeMetre2: this.nombreDeMetre2,
      typeAppartement: this.typeAppartement,
      ville: this.ville,
      locataires: this.locataires,
      nomResidence: this.nomResidence,
      numeroAppartement: this.numeroAppartement,
    };
  }
}

export class ColumnItem implements IColumnItem {
  name: string;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn<DataItem> | null;
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn<DataItem> | null;
  filterMultiple: boolean;
  sortDirections: NzTableSortOrder[];
  width: string;
  left: boolean;
  right: boolean;

  deserialize(input: IColumnItem): ColumnItem {
    if (input) {
      const columnItem = new ColumnItem();
      columnItem.name = input.name;
      columnItem.sortOrder = input.sortOrder;
      columnItem.sortFn = input.sortFn;
      columnItem.listOfFilter = input.listOfFilter;
      columnItem.filterFn = input.filterFn;
      columnItem.filterMultiple = input.filterMultiple;
      columnItem.sortDirections = input.sortDirections;
      columnItem.width = input.width;
      columnItem.left = input.left;
      columnItem.right = input.right;

      return columnItem;
    }
    return null as any;
  }

  deserializeList(input: IColumnItem[]): ColumnItem[] {
    if (input) {
      return input.map((x) => this.deserialize(x));
    }
    return null as any;
  }
  serialize(): IColumnItem {
    return {
      name: this.name,
      sortOrder: this.sortOrder,
      sortFn: this.sortFn,
      listOfFilter: this.listOfFilter,
      filterFn: this.filterFn,
      filterMultiple: this.filterMultiple,
      sortDirections: this.sortDirections,
      width: this.width,
      left: this.left,
      right: this.right,
    };
  }
}
