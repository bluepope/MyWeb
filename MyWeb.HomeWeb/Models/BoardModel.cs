using MyWeb.Lib.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWeb.HomeWeb.Models
{
    public class BoardModel
    {
        public uint Idx { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public uint Reg_User { get; set; }
        public string Reg_Username { get; set; }
        public DateTime Reg_Date { get; set; }
        public uint View_Cnt { get; set; }
        public short Status_Flag { get; set; }

        public static List<BoardModel> GetList(string search)
        {
            using (var db = new MySqlDapperHelper())
            {
                string sql = @"
SELECT
	A.idx
	,A.title
    ,A.reg_user
    ,A.reg_username
    ,A.reg_date
    ,A.view_Cnt
    ,A.status_flag
FROM
	t_board A
WHERE
	A.title LIKE CONCAT('%', IFNULL(@search, ''), '%')
ORDER BY
    A.idx DESC
";
                return db.Query<BoardModel>(sql, new { search = search });
            }
        }

        public static BoardModel Get(uint idx)
        {
            using (var db = new MySqlDapperHelper())
            {
                string sql = @"
SELECT
	A.idx
	,A.title
	,A.contents
    ,A.reg_user
    ,A.reg_username
    ,A.reg_date
    ,A.view_Cnt
    ,A.status_flag
FROM
	t_board A
WHERE
	A.idx = @idx
";
                return db.QuerySingle<BoardModel>(sql, new { idx = idx });
            }
        }

        void CheckContents()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                throw new Exception("제목이 빈칸입니다");
            }
            if (string.IsNullOrWhiteSpace(this.Contents))
            {
                throw new Exception("내용이 빈칸입니다");
            }

            if (string.IsNullOrWhiteSpace(this.Reg_Username))
            {
                throw new Exception("작성자가 없습니다");
            }
        }

        public int Insert()
        {
            CheckContents();

            string sql = @"
INSERT INTO t_board (
	title
	,contents
    ,reg_user
    ,reg_username
    ,reg_date
    ,view_Cnt
    ,status_flag
)
VALUES (
    @title
	,@contents
    ,@reg_user
    ,@reg_username
    ,now()
    ,0
    ,0
)
";
            using (var db = new MySqlDapperHelper())
            {
                return db.Execute(sql, this);
            }
        }

        public int Update()
        {
            CheckContents();

            string sql = @"
UPDATE t_board
SET
    title = @title
	,contents = @contents
WHERE
    idx = @idx
";
            using (var db = new MySqlDapperHelper())
            {
                return db.Execute(sql, this);
            }
        }

        public int Delete()
        {
            string sql = @"
DELETE FROM t_board
WHERE
    idx = @idx
";
            using (var db = new MySqlDapperHelper())
            {
                return db.Execute(sql, this);
            }
        }
    }
}
