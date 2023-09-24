export interface Cot {
  id: number;
  maCot: string;
  noidung: string;
}

export interface Hang {
  id: number;
  maHang: string;
  noidung: string;
}

export interface CauHoiConfig {
  loaiCauHoi: number;
  maCauHoi: string;
  batBuoc?: boolean;
  panelTitle: string;
  tieuDe: string;
  isOther?: boolean;
  labelCauTraLoi: string;
  kichThuocFile: number;
  lstCot: Cot[];
  lstHang: Hang[];
  noidung: string;
}

export interface SurveyConfig {
  idBangKhaoSat: number;
  trangThaiKhaoSat: number;
  lstCauHoi: CauHoiConfig[];
  kqSurvey: string;
  trangThaiKq: number;
}
