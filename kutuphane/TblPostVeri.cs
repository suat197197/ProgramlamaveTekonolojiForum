using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    public class TblPostVeri
    {
        public TblPost TblPostKayitGetir_Id(int Id)
        {
            try
            {
                Baglanti b = new Baglanti();

                using (SqlConnection con = b.BaglantiGetir())
                {
                    var sql = @"SELECT * FROM Kullanici k WHERE Id=@Id";
                    List<TblPost> gelenData = con.Query<TblPost>(sql, new { Id = Id }).ToList();
                    return gelenData[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TblPost? TblPostKayitEkle(TblPost kayit)
        {
            int? Id = 0;
            TblPost eklenen = new TblPost();
            eklenen.Id = 0;
            try
            {
                Baglanti b = new Baglanti();
                
                using (SqlConnection con = b.BaglantiGetir())
                {
                    var sql = @"
			                    INSERT INTO Post
                                           (IdKullanici
                                          ,IdAltKategori
                                          ,IdUstPost
                                          ,Baslik
                                          ,IP
                                          ,KayitTarihi
                                          ,GoruntulenmeSayi
                                          ,Durum)
                                 VALUES          
                                           (@IdKullanici
                                          ,@IdAltKategori
                                          ,@IdUstPost
                                          ,@Baslik
                                          ,@IP
                                          ,@KayitTarihi
                                          ,@GoruntulenmeSayi
                                          ,@Durum)
                                      ;SELECT CAST(SCOPE_IDENTITY() as int)";
                    Id = con.Query<int>(sql
                    , new
                    {
                        IdKullanici=kayit.IdKullanici,
                        IdAltKategori=kayit.IdAltKategori,
                        IdUstPost=kayit.IdUstPost,
                        Baslik=kayit.Baslik,
                        IP=kayit.IP,
                        KayitTarihi = kayit.KayitTarihi,
                        GoruntulenmeSayi = kayit.GoruntulenmeSayi,
                        Durum = kayit.Durum
                    }).FirstOrDefault();
                    eklenen.Id = Id ?? 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eklenen;
        }
        public string TblPostKayitGuncelle(int Id, TblPost kayit)
        {
            string sonuc = "";
            try
            {
                Baglanti b = new Baglanti();
                using (SqlConnection con = b.BaglantiGetir())
                {
                    var sql = @"UPDATE Post
   SET IdKullanici = @IdKullanici
      ,IdAltKategori = @IdAltKategori
      ,IdUstPost = @IdUstPost
      ,Baslik = @Baslik
      ,IP = @IP
      ,KayitTarihi = @KayitTarihi
      ,GoruntulenmeSayi = @GoruntulenmeSayi
      ,Durum = @Durum
 WHERE Id=@Id";
                   con.Execute(sql
                   , new
                   {
                       IdKullanici = kayit.IdKullanici,
                       IdAltKategori = kayit.IdAltKategori,
                       IdUstPost = kayit.IdUstPost,
                       Baslik = kayit.Baslik,
                       IP = kayit.IP,
                       KayitTarihi = DateTime.Now,
                       GoruntulenmeSayi = kayit.GoruntulenmeSayi,
                       Durum = kayit.Durum
                   });
                    sonuc = "Başarılı";
                }
            }
            catch (Exception ex)
            {
                sonuc = ex.Message;
                throw;
            }
            return sonuc;
        }
        public string TblPostDurumDegistir(int Id, int durum)
        {
            string sonuc = "";
            try
            {
                Baglanti b = new Baglanti();
                using (SqlConnection con = b.BaglantiGetir())
                {
                    var sql = @"
			                    UPDATE Post
                                SET Durum=@Durum
                                WHERE Id=@Id";
                    con.Execute(sql
                   , new
                   {
                       Id = Id,
                       Durum = durum
                   });
                    sonuc = "Başarılı";

                }
            }
            catch (Exception ex)
            {
                sonuc = ex.Message;
                throw;

            }

            return sonuc;
        }

        public List<VMAnaSayfa> AnaSayfaVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<VMAnaSayfa> veri = new List<VMAnaSayfa>();
                using (con)
                {
                    var sql = @"SELECT Top 10 ku.Id KullaniciId,ku.TakmaAd,ku.Resim,ku.KayitTarihi KullaniciKayitTarihi,ku.Tip as KullaniciTip
, K.Id AS KonuId, ka.Baslik KategoriBaslik
,ka.Id KategoriId,p.Id PostId ,p.Baslik PostBaslik,p.GoruntulenmeSayi,p.KayitTarihi PostKayitTarihi
,1 PostTip,p.IdUstPost,p.Icerik
FROM Konu k 
INNER JOIN Kategori ka on k.Id=ka.IdKonu
INNER JOIN Post p on p.IdKategori=ka.Id
INNER JOIN Kullanici ku ON ku.Id=p.IdKullanici
WHERE K.Id=1 AND ka.Durum=1 and p.Durum=1
and p.IdUstPost=0
order by p.GoruntulenmeSayi desc";
                    veri = con.Query<VMAnaSayfa>(sql).ToList();
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
