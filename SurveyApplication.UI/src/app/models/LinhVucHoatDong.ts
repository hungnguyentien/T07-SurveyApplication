import { Base } from './Common/Base';

export interface LinhVucHoatDong extends Base {
    id:number;
    maLinhVuc: string;
    tenLinhVuc: string | null;
    moTa: string;
}