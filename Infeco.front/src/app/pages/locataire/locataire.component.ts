import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompactType, DisplayGrid, GridType, GridsterConfig, GridsterItem } from 'angular-gridster2';

@Component({
  selector: 'paiement',
  templateUrl: './locataire.component.html',
  styleUrls: ['./locataire.component.less'],
})
export class LocataireComponent implements OnInit {
  options: GridsterConfig;
  dashboard: Array<GridsterItem>;
  locataireAppartementId?: number;
  locataireId?: number;
  rafraichirQuittanceLoyer: boolean = false;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.options = {
      gridType: GridType.Fit,
      compactType: CompactType.None,
      margin: 1,
      outerMargin: true,
      outerMarginTop: null,
      outerMarginRight: null,
      outerMarginBottom: null,
      outerMarginLeft: null,
      useTransformPositioning: true,
      mobileBreakpoint: 200,
      minCols: 1,
      maxCols: 100,
      minRows: 1,
      maxRows: 100,
      maxItemCols: 100,
      minItemCols: 1,
      maxItemRows: 100,
      minItemRows: 1,
      maxItemArea: 500,
      minItemArea: 1,
      defaultItemCols: 1,
      defaultItemRows: 1,
      fixedColWidth: 105,
      //fixedRowHeight: 100,
      keepFixedHeightInMobile: false,
      keepFixedWidthInMobile: false,
      scrollSensitivity: 10,
      scrollSpeed: 20,
      enableEmptyCellClick: false,
      enableEmptyCellContextMenu: false,
      enableEmptyCellDrop: false,
      enableEmptyCellDrag: false,
      emptyCellDragMaxCols: 50,
      emptyCellDragMaxRows: 50,
      ignoreMarginInRow: false,
      draggable: {
        enabled: false,
      },
      resizable: {
        enabled: false,
      },
      swap: false,
      pushItems: true,
      disablePushOnDrag: false,
      disablePushOnResize: false,
      pushDirections: { north: true, east: true, south: true, west: true },
      pushResizeItems: false,
      displayGrid: DisplayGrid.Always,
      disableWindowResize: false,
      disableWarnings: false,
      scrollToNewItems: false,
    };
    this.dashboard = [
      { cols: 2, rows: 1, y: 0, x: 0 },
      { cols: 2, rows: 1, y: 0, x: 2 },
      { cols: 4, rows: 1, y: 0, x: 2 },
    ];
    const locataireAppartementRoute = this.route.snapshot.paramMap.get('locataireAppartementId');
    const locataireIdRoute = this.route.snapshot.paramMap.get('locataireId');
    if (locataireAppartementRoute && locataireIdRoute) {
      this.locataireAppartementId = +locataireAppartementRoute;
      this.locataireId = +locataireIdRoute;
    }
  }

  refreshQuittanceLoyer(event: any) {
    this.rafraichirQuittanceLoyer = true;
  }
}
