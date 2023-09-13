import { Base } from './Common/Base';
import { Paging } from './Paging';

export interface CreateGuiEmail {
  lstBangKhaoSat: number[];
  lstIdDonVi: number[];
  tieuDe: string;
  noiDung: string;
}

export interface GuiEmail extends Base {
  maGuiEmail: string;
  diaChiNhan: string;
  tieuDe: string;
  noiDung: string;
  trangThai: number;
  thoiGian: Date;
  idBangKhaoSat: number;
  tenBangKhaoSat: string;
  idDonVi: number;
  tenDonVi: string;
  nguoiThucHien: string;
  linkKhaoSat: string;
}

export interface GuiEmailBks {
  idBangKhaoSat: number;
  maBangKhaoSat: string;
  tenBangKhaoSat: string;
  countSendEmail: number;
  countSendThanhCong: number;
  countSendLoi: number;
  countSendThuHoi: number;
  ngayBatDau: Date | null;
  ngayKetThuc: Date | null;
}

export interface PagingGuiEmailBks extends Paging {
  idBanhgKhaoSat: number;
  trangThaiGuiEmail: number | null;
}

export interface GuiLaiGuiEmail {
  guiEmailDto: CreateGuiEmail;
  lstIdGuiMail: number[];
}

export interface ThuHoiGuiEmail {
  lstIdGuiMail: number[];
  diaChiNhan: string;
  tieuDe: string;
  noiDung: string;
}
