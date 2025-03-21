using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kutuphane
{
    public class TblKullaniciVeri
    {
        public TblKullanici TblKullaniciKayitGetir_Id(int Id)
        {
            try
            {
                Baglanti b = new Baglanti();
                using (b.connection)
                {
                    var sql = @"SELECT k.Id ,k.TakmaAd,k.Sifre,k.EPosta,k.IP,k.Tip,k.KayitTarihi,k.GuncellemeTarihi,k.Durum FROM Kullanici k WHERE Id=@Id";
                    List<TblKullanici> gelenData = b.connection.Query<TblKullanici>(sql, new { Id = Id }).ToList();
                    return gelenData[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TblKullanici? TblKullaniciKayitEkle(TblKullanici kayit)
        {
            int? Id = 0;
            TblKullanici eklenen = new TblKullanici();
            eklenen.Id = 0;
            try
            {
                Baglanti b = new Baglanti();
                using (b.connection)
                {
                    var sql = @"
			                    INSERT INTO Kullanici
                               (TakmaAd
                               ,Sifre
                               ,EPosta
                               ,IP
                               ,Tip
                               ,KayitTarihi
                               ,GuncellemeTarihi
                               ,Durum)
                                VALUES
                               (@TakmaAd
                               ,@Sifre,
                               ,@EPosta,
                               ,@IP, 
                               ,@Tip,
                               ,@KayitTarihi,
                               ,@GuncellemeTarihi,
                               ,@Durum);SELECT CAST(SCOPE_IDENTITY() as int)";
                    Id = b.connection.Query<int>(sql
                    , new
                    {
                        TakmaAd = kayit.TakmaAd,
                        Sifre = kayit.Sifre,
                        EPosta = kayit.EPosta,
                        IP = kayit.IP,
                        Tip = kayit.Tip,
                        KayitTarihi = DateTime.Now,
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
        public string TblKullaniciKayitGuncelle(int Id, TblKullanici kayit)
        {
            string sonuc = "";
            try
            {
                Baglanti b = new Baglanti();
                using (b.connection)
                {
                    var sql = @"
			                    UPDATE Kullanici
   SET TakmaAd = @TakmaAd
      ,Sifre = @Sifre
      ,EPosta = @EPosta
      ,IP = @IP
      ,Tip = @Tip
      ,KayitTarihi = @KayitTarihi
      ,GuncellemeTarihi = @GuncellemeTarihi
      ,Durum = 
 WHERE Id=@Id";
                    b.connection.Execute(sql
                   , new
                   {
                       TakmaAd = kayit.TakmaAd,
                       Sifre = kayit.Sifre,
                       EPosta = kayit.EPosta,
                       IP = kayit.IP,
                       Tip = kayit.Tip,
                       GuncellemeTarihi = DateTime.Now,
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
        public string TblKullaniciDurumDegistir(int Id, int durum)
        {
            string sonuc = "";
            try
            {
                Baglanti b = new Baglanti();
                using (b.connection)
                {
                    var sql = @"
			                    UPDATE Kullanici
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
