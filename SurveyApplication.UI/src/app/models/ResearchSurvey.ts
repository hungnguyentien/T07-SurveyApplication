import { Base } from './Common/Base';
import { DonVi } from './CreateUnitAndRep';

export interface PhieuKhaoSatDoanhNghiep {
  tieuDe: string;
  diaChiNhan: string;
  tenBangKhaoSat: string;
  ngayBatDau: string;
  ngayKetThuc: string;
  linkKhaoSat: string;
}

export interface UpdateDoanhNghiep {
  donVi: DoanhNghiep;
  nguoiDaiDien: NguoiDaiDienDnDto;
  idGuiEmail: string;
}

export interface DoanhNghiep {
    idLoaiHinh: number | null;
    idLinhVuc: number | null;
    idTinhTp: number | null;
    idQuanHuyen: number | null;
    idXaPhuong: number | null;
    tenDonVi: string | null;
    diaChi: string | null;
    maSoThue: string | null;
    email: string | null;
    webSite: string | null;
    soDienThoai: string | null;
}

export interface NguoiDaiDienDnDto {
    hoTen: string | null;
    chucVu: string | null;
    soDienThoai: string | null;
    email: string | null;
}