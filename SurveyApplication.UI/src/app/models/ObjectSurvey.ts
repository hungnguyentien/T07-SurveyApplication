import { Base } from './Common/Base';
import { Representative } from './Representative';

export interface ObjectSurvey {
  MaDonVi: string;
  MaLoaiHinh: number;
  MaLinhVuc: number;
  TenDonVi: string;
  DiaChi: string;
  MaSoThue: string;
  Email: string;
  WebSite: string;
  SoDienThoai: string;
  listRepresentative: Representative;
  id: number;
}

export interface DonVi extends Base {
  maDonVi: string | null;
  idLoaiHinh: number | null;
  idLinhVuc: number | null;
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
