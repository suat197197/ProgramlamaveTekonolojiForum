using Dapper;
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

                using (b.connection)
                {
                    var sql = @"SELECT * FROM Kullanici k WHERE Id=@Id";
                    List<TblPost> gelenData = b.connection.Query<TblPost>(sql, new { Id = Id }).ToList();
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
                using (b.connection)
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
                    Id = b.connection.Query<int>(sql
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
                using (b.connection)
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
                    b.connection.Execute(sql
                   , new
                   {
                       IdKullanici=kayit.IdKullanici,
                       IdAltKategori=kayit.IdAltKategori,
                       IdUstPost=kayit.IdUstPost,
                       Baslik=kayit.Baslik,
                       IP=kayit.IP,
                       KayitTarihi=DateTime.Now,
                       GoruntulenmeSayi=kayit.GoruntulenmeSayi,
                       Durum=kayit.Durum
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
                using (b.connection)
                {
                    var sql = @"
			                    UPDATE Post
                                SET Durum=@Durum
                                WHERE Id=@Id";
                    b.connection.Execute(sql
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
    }
}
