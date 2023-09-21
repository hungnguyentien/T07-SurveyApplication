import { Base } from './Common/Base';

export interface CreateUnitAndRep {
  donViDto: ObjectSurvey;
  nguoiDaiDienDto: Representative;
}

export interface ObjectSurvey extends Base {
  IdLoaiHinh: number;
  IdLinhVuc: number;
  TenDonVi: string;
  DiaChi: string;
  MaSoThue: string;
  Email: string;
  WebSite: string;
  SoDienThoai: string;
}
export interface Representative extends Base {
  SoDienThoai: string;
  HoTen: string;
  ChucVu: string;
  Email: string;
  MoTa: string;
}

export interface DonVi extends Base {
  maDonVi: string | null;
  idLoaiHinh: number | null;
  idLinhVuc: number | null;
  idTinhTp: number | null;
  tinhTp: string | null;
  idQuanHuyen: number | null;
  idXaPhuong: number | null;
  tenDonVi: string | null;
  diaChi: string | null;
  maSoThue: string | null;
  email: string | null;
  webSite: string | null;
  soDienThoai: string | null;
  idDonVi: number | null;
  idNguoiDaiDien: number | null;
  emailDonVi: string | null;
  soDienThoaiDonVi: string | null;
  hoTen: string | null;
  tenLoaiHinh: string | null;
  chucVu: string | null;
  moTa: string | null;
  emailNguoiDaiDien: string | null;
  soDienThoaiNguoiDaiDien: string | null;
}
