export interface BaseQuerieResponse<T> {
  pageIndex: number;
  pageSize: number;
  keyword: number;
  totalFilter: number;
  // totalCount:number;
  data: T[];
}
