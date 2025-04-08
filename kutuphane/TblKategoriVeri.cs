using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    public class TblKAtegoriVeri
    {

        public List<TblKategori> TumKategoriVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<TblKategori> veri = new List<TblKategori>();
                using (con)
                {
                    var sql = @"select ka.Id as IdKategori,uk.Id as IdUstKategori ,
ka.Baslik KategoriAdi,uk.Adi as UstKategoriAdi,k.Id as IdKonu
,k.Adi as KonuAdi
  from
konu k Inner join UstKategori uk on k.Id=uk.IdKonu
Inner join Kategori ka on ka.IdUstKategori=uk.Id 
where 
ka.Durum=1
";
                    veri = con.Query<TblKategori>(sql).ToList();
                    return veri;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TblKategori> KonuKategoriVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<TblKategori> veri = new List<TblKategori>();
                using (con)
                {
                    var sql = @"select ka.Id as IdKategori,uk.Id as IdUstKategori ,
ka.Baslik KategoriAdi,uk.Adi as UstKategoriAdi,k.Id as IdKonu
,k.Adi as KonuAdi
  from
konu k Inner join UstKategori uk on k.Id=uk.IdKonu
Inner join Kategori ka on ka.IdUstKategori=uk.Id 
where
ka.Durum=1 and k.Id=2
";
                    veri = con.Query<TblKategori>(sql).ToList();
                    return veri;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TblKategori> SoruKategoriVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<TblKategori> veri = new List<TblKategori>();
                using (con)
                {
                    var sql = @"select ka.Id as IdKategori,uk.Id as IdUstKategori ,
ka.Baslik KategoriAdi,uk.Adi as UstKategoriAdi,k.Id as IdKonu
,k.Adi as KonuAdi
  from
konu k Inner join UstKategori uk on k.Id=uk.IdKonu
Inner join Kategori ka on ka.IdUstKategori=uk.Id 
where 
ka.Durum=1 and k.Id=1
";
                    veri = con.Query<TblKategori>(sql).ToList();
                    return veri;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
