import { Base } from './Common/Base';

export interface TableSurvey extends Base {
  id:number;
  maBangKhaoSat: string;
  idLoaiHinh: number;
  idDotKhaoSat: string | null;
  tenBangKhaoSat: string | null;
  moTa: string | null;
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
  trangThai: number | null;
  tenDotKhaoSat: string | null;
  tenLoaiHinh: string | null;
}

export interface CreateUpdateBangKhaoSat extends Base {
  maBangKhaoSat: string;
  idLoaiHinh: number | null;
  idDotKhaoSat: number | null;
  tenBangKhaoSat: string | null;
  moTa: string | null;
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
  trangThai: number | null;
  bangKhaoSatCauHoi: BangKhaoSatCauHoiDto[] | null;
}

export interface BangKhaoSatCauHoiDto extends Base {
  idCauHoi: number;
  priority: number | null;
  isRequired: boolean | null;
  maCauHoi: string | null;
  tieuDe: string | null;
}
