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

export interface BaoCaoCauHoi {
  countDonViMoi: number;
  countDonViTraLoi: number;
  countDonViSo: number;
  countDonViBo: number;
  countDonViNganh: number;
}


export interface DashBoardRequest {
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
}

export interface DashBoard {
  ngayBatDau: string | null;
  ngayKetThuc: string | null;
}
