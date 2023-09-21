import { Base } from "./Common/Base";

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

// export interface BaoCaoCauHoi {
//   countDonViMoi: number;
//   countDonViTraLoi: number;
//   countDonViSo: number;
//   countDonViBo: number;
//   countDonViNganh: number;
// }


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
    idCauHoi: number;
    cauHoiTraLoi: BaoCaoCauHoi[] | null;
}