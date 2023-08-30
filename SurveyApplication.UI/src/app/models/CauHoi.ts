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
