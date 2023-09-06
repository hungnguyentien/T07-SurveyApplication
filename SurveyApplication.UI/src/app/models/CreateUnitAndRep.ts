import { Base } from './Common/Base';

export interface CreateUnitAndRep {
  donViDto: ObjectSurvey;
  nguoiDaiDienDto: Representative;
}

export interface ObjectSurvey extends Base{
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
