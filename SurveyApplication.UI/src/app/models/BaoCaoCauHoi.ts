import { Base } from './Common/Base';

export interface CreateBaoCaoCauHoi {
  idCauHoi: number;
  idDotKhaoSat: number;
  idBangKhaoSat: number;
  idLoaiHinhDonVi: number;
  maCauHoi: string | null;
  cauHoi: string | null;
  maCauHoiPhu: string | null;
  cauHoiPhu: string | null;
  maCauTraLoi: string | null;
  cauTraLoi: string | null;
  loaiCauHoi: number;
  tenDaiDienCq: string | null;
}

export interface CreateBaoCaoCauHoiCommand {
  lstBaoCaoCauHoi: CreateBaoCaoCauHoi[];
  idGuiEmail: string;
}

export interface BaoCaoCauHoiRequest {
  idDotKhaoSat: number;
  idBangKhaoSat: number;
  idLoaiHinhDonVi: number | null;
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
}

export interface DashBoardRequest {
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
}

export interface DashBoard {
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
}

export interface BaoCaoCauHoi extends Base {
  idCauHoi: number;
  idDotKhaoSat: number;
  idBangKhaoSat: number;
  idDonVi: number;
  idLoaiHinhDonVi: number;
  tenLoaiHinhDonVi: string | null;
  maCauHoi: string;
  cauHoi: string;
  maCauHoiPhu: string | null;
  cauHoiPhu: string | null;
  maCauTraLoi: string | null;
  cauTraLoi: string | null;
  loaiCauHoi: number;
  tenDaiDienCq: string;
  dauThoiGian: string | null;
  countDonViMoi: number;
  countDonViTraLoi: number;
  countDonViSo: number;
  countDonViBo: number;
  countDonViNganh: number;
  diaChi: string | null;
  soLuotChon: number | null;
  tyLe: number | null;
  listCauHoiTraLoi: ListCauHoiTraLoi[] | null;
}

export interface ListCauHoiTraLoi {
  idCauHoi: number | null;
  loaiCauHoi: number;
  tenCauHoi: string | null;
  cauHoiTraLoi: CauHoiTraLoi[] | null;
}

export interface CauHoiTraLoi {
  cauTraLoi: string | null;
  soLuotChon: number;
  tyLe: number;
}

export interface BaoCaoCauHoiChiTietRequest {
  idDotKhaoSat: number;
  idBangKhaoSat: number;
  idLoaiHinhDonVi: number | null;
  idDonVi: number | null;
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
  pageIndex: number;
  pageSize: number;
  keyword: string | null;
}

export interface CauHoiCauTraLoiChiTiet {
  cauHoi: string;
  loaiCauHoi: string;
  cauTraLoi: string[];
}

export interface BaoCaoCauHoiChiTiet {
  idDonVi: number;
  idBangKhaoSat: number;
  idDotKhaoSat: number;
  dauThoiGian: string;
  lstCauHoiCauTraLoi: CauHoiCauTraLoiChiTiet[];
}

export interface FileQuestion {
  content: string;
  name: string;
  type: string;
}
