import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { Parametre, ParametreChange } from '@models/parametrages';
import { ISelect } from '@models/select';
import { NzDrawerRef } from 'ng-zorro-antd/drawer';
import { of } from 'rxjs';

@Component({
  selector: 'molecules-parameters',
  templateUrl: './molecules-parameters.component.html',
  styleUrls: ['./molecules-parameters.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MoleculesParametersComponent implements OnInit {
  @Input() libelleBouton: string;
  @Input() value: Parametre[];

  data: Parametre[] = [];

  constructor(private drawerRef: NzDrawerRef<string>) {}

  ngOnInit() {
    this.value.map((u) => {
      const parametre = new Parametre();
      parametre.id = u.id;
      parametre.valeur = u.valeur;
      parametre.unite = u.unite;
      parametre.libelle = u.libelle;
      this.data = [...this.data, parametre];
    });
  }

  changeValeur(event: ParametreChange) {
    if (event) {
      const valeurRecherche = this.data.find((u) => u.id === event.id);
      if (valeurRecherche) {
        valeurRecherche.valeur = +event.valeur;
      }
    }
  }

  toObservable(item: ISelect[]) {
    return of(item);
  }

  close(): void {
    this.drawerRef.close(this.data);
  }
}
