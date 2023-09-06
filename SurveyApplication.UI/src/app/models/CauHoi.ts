import { Base } from './Common/Base';
import { Cot, Hang } from './SurveyConfig';

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
export interface CreateUpdateCauHoi {
  id: number;
  maCauHoi: string;
  loaiCauHoi: number;
  batBuoc: boolean | null;
  tieuDe: string;
  noidung: string;
  soLuongFileToiDa: number;
  kichThuocFile: number;
  isOther: boolean | null;
  labelCauTraLoi: string;
  priority: number;
  lstCot: Cot[]; //Câu hỏi phụ
  lstHang: Hang[]; // Câu trả lời
}