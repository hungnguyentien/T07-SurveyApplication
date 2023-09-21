export interface BaseQuerieResponse<T> {
  pageIndex: number;
  pageSize: number;
  keyword: number;
  totalFilter: number;
  data: T[];
}
