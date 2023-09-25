using System.Collections.Generic;

namespace SurveyApplication.Utility.Enums
{
    public static class MapEnum
    {
        public static Dictionary<int, List<EnumPermission.Type>> MatrixPermission
        {
            get
            {
                var map = new Dictionary<int, List<EnumPermission.Type>>
                {
                    {
                        (int)EnumModule.Code.Dashboard,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlLhDv,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlDv,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlCh,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlDks,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlKs,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlGm,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.TkKs,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.LvHd,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlTt,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlQh,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlPx,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlNq,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlTk,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlMd,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlNdd,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.QlIp,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    },

                    {
                        (int)EnumModule.Code.Qlsl,
                        new List<EnumPermission.Type>
                        {
                            EnumPermission.Type.Read, EnumPermission.Type.Create, EnumPermission.Type.Update,
                            EnumPermission.Type.Deleted, EnumPermission.Type.Import, EnumPermission.Type.Export
                        }
                    }
                };

                return map;
            }
        }
    }
}