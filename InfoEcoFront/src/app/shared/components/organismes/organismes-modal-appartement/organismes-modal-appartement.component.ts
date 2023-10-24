import { Component, OnInit } from '@angular/core';
import { DonneesReferencesService } from '@core/services/donnees-references.service';

@Component({
  selector: 'organismes-modal-appartement',
  templateUrl: './organismes-modal-appartement.component.html',
  styleUrls: ['./organismes-modal-appartement.component.less'],
})
export class OrganismesModalAppartementComponent implements OnInit {
  constructor(private donneesReferencesService: DonneesReferencesService) {}

  ngOnInit() {}
}
