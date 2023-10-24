import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { ResultatApprocheAffichage } from '@models/approcheEpargne';
import { TypeFiscalite } from '@models/type-fiscalite';
import { Subject } from 'rxjs';
import { Workbook, Worksheet } from 'exceljs';
import * as saveAs from 'file-saver';
import { DatePipe } from '@angular/common';
import { IEcheances } from '@models/pretFlash';
import * as moment from 'moment';
import { DataService } from '@core/services/data.service';
import { DonneesSimulations, DonneesTelechargement } from '@models/donneesSimulations';
import { recuperationTVA } from '@models/select-list';

@Component({
  selector: 'molecules-export-excel',
  templateUrl: './molecules-export-excel.component.html',
  styleUrls: ['./molecules-export-excel.component.less'],
})
export class MoleculesExportExcelComponent {
  @Input() typeFiscalite: TypeFiscalite;
  @Input() typePretAmortissable: string;
  @Input() nomFiscalite: string;
  @Input() element: ResultatApprocheAffichage[];
  @Input() estUnPretFlash: boolean = false;
  @Input() echeancierVisibleMensuel: boolean;
  @Input() echeances: IEcheances[];
  @Input() lmpnModeRecuperationTVA: string;

  filePath = 'assets/nom-du-fichier.xlsx';
  colorPaire: string = 'd2deef';
  colorImpaire: string = 'eaeff7';
  color: string;
  loading: boolean;
  anneeJour: number;

  private destroy$ = new Subject<void>();

  constructor(private datePipe: DatePipe, private dataService: DataService) {}

  ngOnInit() {
    this.anneeJour = new Date().getFullYear() - 1;
  }

  downloadData() {
    const workbook = new Workbook();
    const worksheet = workbook.addWorksheet('Tableau amortissement', {
      properties: { tabColor: { argb: 'FF00FF00' } },
      views: [
        {
          ySplit: 5,
          activeCell: 'A1',
          showGridLines: false,
        },
      ],
    });

    if (!this.estUnPretFlash) {
      if (this.typeFiscalite === TypeFiscalite.pinel) {
        this.genereApprochePinel(worksheet);
      }

      if (this.typeFiscalite === TypeFiscalite.regimeCommun) {
        this.genereApprocheRegimeCommun(worksheet);
      }
      if (this.typeFiscalite === TypeFiscalite.lmnp) {
        this.genereApprocheLMNP(worksheet);
      }
    } else {
      this.genereApprochePretFlash(worksheet);
    }

    // Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data: any) => {
      const blob = new Blob([data], {
        type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
      });
      saveAs(blob, 'tableau_amortissement.xlsx');
    });

    const data = new DonneesTelechargement();
    if (!this.estUnPretFlash) {
      data.typeApproche = 'Approche épargne';
      data.typeSimulationId = this.typeFiscalite;
    } else {
      data.typeApproche = 'Prêt flash';
      data.detail = this.typePretAmortissable;
    }
    data.dateTelechargement = new Date();

    const donneesSimulations = new DonneesSimulations();
    donneesSimulations.donneesTelechargement = data;

    this.dataService.sauvergardeDonneesApproches(donneesSimulations).subscribe();
  }

  genereApprochePinel(worksheet: Worksheet) {
    worksheet.addRow([`Taux marginal d’imposition : ${this.element[0].tmi} %`, '', '', '']);

    worksheet.addRow(['', 'Pinel 6 ans', 'Pinel 9 ans', 'Pinel 12 ans']);
    this.element.forEach((u) => {
      if (u.uniteDeMesure === '%') {
        worksheet.addRow([u.libelle, u.valeur6ans / 100, u.valeur9ans / 100, u.valeur12ans / 100]);
      } else {
        worksheet.addRow([u.libelle, u.valeur6ans, u.valeur9ans, u.valeur12ans]);
      }
    });

    worksheet.mergeCells('A1:D1');
    worksheet.mergeCells('A16:D16');
    worksheet.mergeCells('A9:D9');

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        cell.fill = {
          type: 'pattern',
          pattern: 'solid',
          fgColor: { argb: 'FFFFFFFF' }, // Utilisez 'FFFFFFFF' pour le blanc
        };
        if (colNumber !== 1) {
          if (rowNumber == 9 || rowNumber == 1 || rowNumber == 16) {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' } };
          }
        } else {
          if (rowNumber == 2 || rowNumber == 3) {
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.font = { color: { argb: '000000' } };
          }
        }
        if (rowNumber === 1) {
          cell.border = {
            bottom: { style: 'thin' },
          };
        } else {
          cell.border = {
            top: { style: 'thin' },
            left: { style: 'thin' },
            bottom: { style: 'thin' },
            right: { style: 'thin' },
          };
        }
      });
    });

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        if (rowNumber !== 9 && rowNumber !== 16 && rowNumber !== 15 && colNumber > 1) {
          cell.numFmt = '#,##0.00 €';
        } else {
          if (rowNumber === 15 && colNumber > 1) {
            cell.numFmt = '#,##0.00 %';
          }
        }
      });
    });

    worksheet.getCell('A9').alignment = { horizontal: 'center' };
    worksheet.getCell('A16').alignment = { horizontal: 'center' };
    worksheet.getColumn(1).width = 40;
    worksheet.getColumn(2).width = 12;
    worksheet.getColumn(3).width = 12;
    worksheet.getColumn(4).width = 12;
  }

  genereApprocheRegimeCommun(worksheet: Worksheet) {
    worksheet.addRow([`Taux marginal d’imposition : ${this.element[0].tmi} %`]);

    this.element.forEach((u) => {
      if (u.uniteDeMesure === '%') {
        worksheet.addRow([u.libelle, u.valeur6ans / 100]);
      } else {
        worksheet.addRow([u.libelle, u.valeur6ans]);
      }
    });

    worksheet.mergeCells('A8:B8');
    worksheet.mergeCells('A1:B1');
    worksheet.mergeCells('A14:B14');
    worksheet.getCell('A8').alignment = { horizontal: 'center' };
    worksheet.getCell('A14').alignment = { horizontal: 'center' };
    worksheet.getCell('A1').alignment = { horizontal: 'center' };
    worksheet.getColumn(1).width = 40;
    worksheet.getColumn(2).width = 12;

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        cell.fill = {
          type: 'pattern',
          pattern: 'solid',
          fgColor: { argb: 'FFFFFFFF' }, // Utilisez 'FFFFFFFF' pour le blanc
        };
        if (colNumber !== 1) {
          if (rowNumber === 8 || rowNumber === 14 || rowNumber === 1) {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' } };
          }
        } else {
          if (rowNumber === 2) {
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.font = { color: { argb: '000000' } };
          }
        }
        if (rowNumber === 1) {
          cell.border = {
            bottom: { style: 'thin' },
          };
        } else {
          cell.border = {
            top: { style: 'thin' },
            left: { style: 'thin' },
            bottom: { style: 'thin' },
            right: { style: 'thin' },
          };
        }
      });
    });

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        if (rowNumber !== 8 && rowNumber !== 14 && rowNumber !== 13 && colNumber > 1) {
          cell.numFmt = '#,##0.00 €';
        } else {
          if (rowNumber === 13 && colNumber > 1) {
            cell.numFmt = '#,##0.00 %';
          }
        }
      });
    });
  }

  genereApprocheLMNP(worksheet: Worksheet) {
    worksheet.addRow([`Taux marginal d’imposition : ${this.element[0].tmi} %`]);

    this.element.forEach((u) => {
      if (u.uniteDeMesure === '%') {
        worksheet.addRow([u.libelle, u.valeur6ans / 100]);
      } else {
        worksheet.addRow([u.libelle, u.valeur6ans]);
      }
    });

    if (this.lmpnModeRecuperationTVA === 'TVA_RECUP_INVESTISSEUR') {
      worksheet.mergeCells('A9:B9');
      worksheet.mergeCells('A1:B1');
      worksheet.mergeCells('A18:B18');
      worksheet.getCell('A9').alignment = { horizontal: 'center' };
      worksheet.getCell('A18').alignment = { horizontal: 'center' };
      worksheet.getCell('A1').alignment = { horizontal: 'center' };
    }
    if (this.lmpnModeRecuperationTVA === 'TVA_RECUP_PROMOTEUR') {
      worksheet.mergeCells('A10:B10');
      worksheet.mergeCells('A1:B1');
      worksheet.mergeCells('A18:B18');
      worksheet.getCell('A10').alignment = { horizontal: 'center' };
      worksheet.getCell('A18').alignment = { horizontal: 'center' };
      worksheet.getCell('A1').alignment = { horizontal: 'center' };
    }
    if (this.lmpnModeRecuperationTVA === 'TVA_PAS_RECUP') {
      worksheet.mergeCells('A9:B9');
      worksheet.mergeCells('A1:B1');
      worksheet.mergeCells('A17:B17');
      worksheet.getCell('A9').alignment = { horizontal: 'center' };
      worksheet.getCell('A17').alignment = { horizontal: 'center' };
      worksheet.getCell('A1').alignment = { horizontal: 'center' };
    }

    worksheet.getColumn(1).width = 60;
    worksheet.getColumn(2).width = 12;

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        cell.fill = {
          type: 'pattern',
          pattern: 'solid',
          fgColor: { argb: 'FFFFFFFF' }, // Utilisez 'FFFFFFFF' pour le blanc
        };
        if (colNumber !== 1) {
          if (
            (this.lmpnModeRecuperationTVA === 'TVA_RECUP_PROMOTEUR' && (rowNumber === 10 || rowNumber === 18 || rowNumber === 1)) ||
            (this.lmpnModeRecuperationTVA === 'TVA_RECUP_INVESTISSEUR' && (rowNumber === 9 || rowNumber === 18 || rowNumber === 1)) ||
            (this.lmpnModeRecuperationTVA === 'TVA_PAS_RECUP' && (rowNumber === 9 || rowNumber === 17 || rowNumber === 1))
          ) {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.style.alignment = { horizontal: 'center' };
            cell.font = { color: { argb: '000000' } };
          }
        } else {
          if (rowNumber === 2) {
            cell.font = { color: { argb: '000000' }, bold: true };
          } else {
            cell.font = { color: { argb: '000000' } };
          }
        }
        if (rowNumber === 1) {
          cell.border = {
            bottom: { style: 'thin' },
          };
        } else {
          cell.border = {
            top: { style: 'thin' },
            left: { style: 'thin' },
            bottom: { style: 'thin' },
            right: { style: 'thin' },
          };
        }
      });

      if (this.lmpnModeRecuperationTVA === 'TVA_PAS_RECUP') {
        worksheet.getRow(17).height = 46;
        worksheet.getCell('A17').alignment = { horizontal: 'center', vertical: 'middle', shrinkToFit: true, wrapText: true };
      } else {
        worksheet.getRow(18).height = 46;
        worksheet.getCell('A18').alignment = { horizontal: 'center', vertical: 'middle', shrinkToFit: true, wrapText: true };
      }
    });

    if (this.lmpnModeRecuperationTVA === 'TVA_RECUP_PROMOTEUR') {
      worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
        row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
          if (rowNumber !== 21 && rowNumber !== 18 && rowNumber !== 13 && rowNumber !== 10 && colNumber > 1) {
            cell.numFmt = '#,##0.00 €';
          } else {
            if ((rowNumber === 13 || rowNumber === 21) && colNumber > 1) {
              cell.numFmt = '#,##0.00 %';
            }
          }
        });
      });
    } else if (this.lmpnModeRecuperationTVA === 'TVA_RECUP_INVESTISSEUR') {
      worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
        row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
          if (rowNumber !== 21 && rowNumber !== 18 && rowNumber !== 13 && rowNumber !== 9 && colNumber > 1) {
            cell.numFmt = '#,##0.00 €';
          } else {
            if ((rowNumber === 13 || rowNumber === 21) && colNumber > 1) {
              cell.numFmt = '#,##0.00 %';
            }
          }
        });
      });
    } else if (this.lmpnModeRecuperationTVA === 'TVA_PAS_RECUP') {
      worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
        row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
          if (rowNumber !== 20 && rowNumber !== 17 && rowNumber !== 12 && rowNumber !== 9 && colNumber > 1) {
            cell.numFmt = '#,##0.00 €';
          } else {
            if ((rowNumber === 12 || rowNumber === 20) && colNumber > 1) {
              cell.numFmt = '#,##0.00 %';
            }
          }
        });
      });
    }
  }

  calculDateEcheancier(mois: number) {
    let now = moment();
    return now.add(mois, 'months').format();
  }

  genereApprochePretFlash(worksheet: Worksheet) {
    worksheet.addRow([
      this.echeancierVisibleMensuel ? 'Echances' : 'Année',
      "Montant de l'échéance",
      'Capital amorti',
      'Intérêts payés',
      'Assurance payée',
      'Capital restant dû',
    ]);
    if (!this.echeancierVisibleMensuel) {
      this.echeances.forEach((u) => {
        worksheet.addRow([this.anneeJour + u.indice, u.montant, u.capital, u.interets, u.assurance, u.capitalRestantDu]);
      });
    } else {
      this.echeances.forEach((u) => {
        worksheet.addRow([
          this.datePipe.transform(this.calculDateEcheancier(u.indice - 1), 'dd/MM/yyyy'),
          u.montant,
          u.capital,
          u.interets,
          u.assurance,
          u.capitalRestantDu,
        ]);
      });
    }

    worksheet.eachRow({ includeEmpty: true }, (row, rowNumber) => {
      row.eachCell({ includeEmpty: true }, (cell, colNumber) => {
        cell.fill = {
          type: 'pattern',
          pattern: 'solid',
          fgColor: { argb: 'FFFFFFFF' }, // Utilisez 'FFFFFFFF' pour le blanc
        };

        if (rowNumber !== 1 && colNumber > 1) {
          cell.numFmt = '#,##0.00 €';
        }
        cell.style.alignment = { horizontal: 'center' };
        cell.border = {
          top: { style: 'thin' },
          left: { style: 'thin' },
          bottom: { style: 'thin' },
          right: { style: 'thin' },
        };
      });
    });

    if (!this.echeancierVisibleMensuel) {
      worksheet.getColumn(1).width = 8;
    } else {
      worksheet.getColumn(1).width = 11;
    }

    worksheet.getColumn(2).width = 21;
    worksheet.getColumn(3).width = 13;
    worksheet.getColumn(4).width = 13;
    worksheet.getColumn(5).width = 15;
    worksheet.getColumn(6).width = 16;
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
