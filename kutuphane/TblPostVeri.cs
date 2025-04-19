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
        public List<TblPostlar> TblTumPostKayitGetir_Id(int PostId)
        {
            try
            {
                Baglanti b = new Baglanti();

                using (SqlConnection con = b.BaglantiGetir())
                {
                    var sql = @"select t.*,k.Id KullaniciId,k.TakmaAd ,k.Resim,t1.MesajSayi,t2.BegenmeSayi from 
(select * from Post p
where p.Id=@PostId
and p.IdUstPost=0 and p.Durum=1
union 
select * from Post p
where p.IdUstPost=@PostId 
and p.Durum=1
) as t 
Inner join Kullanici k on k.Id=t.IdKullanici
Inner join (
select p.IdKullanici,count(p.Id) as MesajSayi from
Post p 
group by p.IdKullanici
) as t1 on t1.IdKullanici=t.IdKullanici
Inner join (
select p.IdKullanici,sum(p.BegenmeSayi) as BegenmeSayi from
Post p 
group by p.IdKullanici

)  as t2 on t2.IdKullanici=t.IdKullanici

order by t.KayitTarihi asc";
                    List<TblPostlar> gelenData = con.Query<TblPostlar>(sql, new { PostId = PostId }).ToList();
                    return gelenData;
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
                                          ,IdKategori
                                          ,IdUstPost
                                          ,Baslik
                                          ,IP
                                          ,Icerik
                                          ,KayitTarihi
                                          ,GoruntulenmeSayi
                                          ,Durum)
                                 VALUES          
                                           (@IdKullanici
                                          ,@IdKategori
                                          ,@IdUstPost
                                          ,@Baslik
                                          ,@IP
                                           ,@Icerik
                                          ,@KayitTarihi
                                          ,@GoruntulenmeSayi
                                          ,@Durum)
                                      ;SELECT CAST(SCOPE_IDENTITY() as int)";
                    Id = con.Query<int>(sql
                    , new
                    {
                        IdKullanici=kayit.IdKullanici,
                        IdKategori=kayit.IdKategori,
                        IdUstPost=kayit.IdUstPost,
                        Baslik=kayit.Baslik,
                        IP=kayit.IP,
                        KayitTarihi = kayit.KayitTarihi,
                        GoruntulenmeSayi = kayit.GoruntulenmeSayi,
                        Durum = kayit.Durum,
                        Icerik=kayit.Icerik
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
                       IdKategori = kayit.IdKategori,
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

        public List<VMPost> AnaSayfaVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<VMPost> veri = new List<VMPost>();
                using (con)
                {
                    var sql = @"with bilgi as(
SELECT Top 10 ku.Id KullaniciId,ku.TakmaAd,ku.Resim,ku.KayitTarihi KullaniciKayitTarihi,ku.Tip as KullaniciTip
, K.Id AS KonuId, ka.Baslik KategoriBaslik
,ka.Id KategoriId,p.Id PostId ,p.Baslik PostBaslik,p.GoruntulenmeSayi,p.KayitTarihi PostKayitTarihi
,1 PostTip,p.IdUstPost,p.Icerik,u.Id IdUstKategori,u.Adi UstKategoriAdi
FROM Konu k 
INNER JOIN Kategori ka on k.Id=ka.IdKonu
INNER JOIN Post p on p.IdKategori=ka.Id
INNER JOIN Kullanici ku ON ku.Id=p.IdKullanici
INNER JOIN UstKategori u on u.IdKonu=k.Id and u.Id=ka.IdUstKategori
WHERE ka.Durum=1 and p.Durum=1
and p.IdUstPost=0
order by p.GoruntulenmeSayi desc
)

select b1.*,isnull(  t.yorumSayi,0) YorumSayi  from bilgi b1 left join ( 
select p.Id, count(p.Id) as YorumSayi from bilgi b 
Inner join Post p on b.IdUstPost=p.Id
group by p.Id ) as t
on b1.PostId=t.Id
order by b1.GoruntulenmeSayi desc
";
                    veri = con.Query<VMPost>(sql).ToList();
                    return veri;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VMPost> SorularSayfaVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<VMPost> veri = new List<VMPost>();
                using (con)
                {
                    var sql = @"with bilgi as(
SELECT Top 10 ku.Id KullaniciId,ku.TakmaAd,ku.Resim,ku.KayitTarihi KullaniciKayitTarihi,ku.Tip as KullaniciTip
, K.Id AS KonuId, ka.Baslik KategoriBaslik
,ka.Id KategoriId,p.Id PostId ,p.Baslik PostBaslik,p.GoruntulenmeSayi,p.KayitTarihi PostKayitTarihi
,1 PostTip,p.IdUstPost,p.Icerik,u.Id IdUstKategori,u.Adi UstKategoriAdi

FROM Konu k 
INNER JOIN Kategori ka on k.Id=ka.IdKonu
INNER JOIN Post p on p.IdKategori=ka.Id
INNER JOIN Kullanici ku ON ku.Id=p.IdKullanici
INNER JOIN UstKategori u on u.IdKonu=k.Id and u.Id=ka.IdUstKategori

WHERE ka.IdKonu=1 and ka.Durum=1 and p.Durum=1
and p.IdUstPost=0
order by p.GoruntulenmeSayi desc
)

select b1.*,isnull(  t.yorumSayi,0) YorumSayi  from bilgi b1 left join ( 
select p.Id, count(p.Id) as YorumSayi from bilgi b 
Inner join Post p on b.IdUstPost=p.Id
group by p.Id ) as t
on b1.PostId=t.Id
order by b1.GoruntulenmeSayi desc
";
                    veri = con.Query<VMPost>(sql).ToList();
                    return veri;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VMPost> KonularSayfaVeriGetir()
        {
            try
            {
                Baglanti b = new Baglanti();
                SqlConnection con = b.BaglantiGetir();
                List<VMPost> veri = new List<VMPost>();
                using (con)
                {
                    var sql = @"with bilgi as(
SELECT Top 10 ku.Id KullaniciId,ku.TakmaAd,ku.Resim,ku.KayitTarihi KullaniciKayitTarihi,ku.Tip as KullaniciTip
, K.Id AS KonuId, ka.Baslik KategoriBaslik
,ka.Id KategoriId,p.Id PostId ,p.Baslik PostBaslik,p.GoruntulenmeSayi,p.KayitTarihi PostKayitTarihi
,1 PostTip,p.IdUstPost,p.Icerik,u.Id IdUstKategori,u.Adi UstKategoriAdi

FROM Konu k 
INNER JOIN Kategori ka on k.Id=ka.IdKonu
INNER JOIN Post p on p.IdKategori=ka.Id
INNER JOIN Kullanici ku ON ku.Id=p.IdKullanici
INNER JOIN UstKategori u on u.IdKonu=k.Id and u.Id=ka.IdUstKategori

WHERE ka.IdKonu=2 and ka.Durum=1 and p.Durum=1
and p.IdUstPost=0
order by p.GoruntulenmeSayi desc
)

select b1.*,isnull(  t.yorumSayi,0) YorumSayi  from bilgi b1 left join ( 
select p.Id, count(p.Id) as YorumSayi from bilgi b 
Inner join Post p on b.IdUstPost=p.Id
group by p.Id ) as t
on b1.PostId=t.Id
order by b1.GoruntulenmeSayi desc
";
                    veri = con.Query<VMPost>(sql).ToList();
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
