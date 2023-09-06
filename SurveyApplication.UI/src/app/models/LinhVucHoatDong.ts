import { Base } from './Common/Base';

export interface LinhVucHoatDong extends Base {
    maLinhVuc: string;
    tenLinhVuc: string | null;
}