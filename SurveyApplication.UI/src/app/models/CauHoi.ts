import { Base } from './Common/Base';

export interface CauHoi extends Base {
  loaiCauHoi: number;
  maCauHoi: string;
  batBuoc: boolean | null;
  tieuDe: string;
  isOther: boolean | null;
  labelCauTraLoi: string;
  kichThuocFile: number;
  loaiCauHoiText: string;
}
export interface CreateCauHoi {
  maCauHoi: string;
  loaiCauHoi: number;
  batBuoc: boolean | null;
  tieuDe: string;
  noiDung: string;
  soLuongFileToiDa: number;
  kichThuocFile: number;
  isOther: boolean | null;
  labelCauTraLoi: string;
  priority: number;
}

export interface UpdateCauHoi {
  id: number;
  maCauHoi: string;
  loaiCauHoi: number;
  batBuoc: boolean | null;
  tieuDe: string;
  noiDung: string;
  soLuongFileToiDa: number;
  kichThuocFile: number;
  isOther: boolean | null;
  labelCauTraLoi: string;
  priority: number;
}
