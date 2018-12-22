using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameWork.Core
{
    public class ClientMaster
    {
        public int Id { get; set; }
        public string ClientCode { get; set; }
        public string BranchName { get; set; }
    }

    public static class ClientList
    {
        public static ClientMaster DEMO_VERSION = new ClientMaster { Id = 49, ClientCode = "TEST_DEMO", BranchName = "Airport Road" };

        public static ClientMaster CKD_GT_ROAD = new ClientMaster { Id = 1, ClientCode = "CKDCS001", BranchName = "G.T. Road" };
        public static ClientMaster CKD_MAJITHA_BYPASS = new ClientMaster { Id = 4, ClientCode = "CKDCS002", BranchName = "Majitha Byepass" };
        public static ClientMaster CKD_BASANT_AVENUE = new ClientMaster { Id = 5, ClientCode = "CKDCS003", BranchName = "Basant Avenue" };
        public static ClientMaster CKD_GOLDEN_AVENUE = new ClientMaster { Id = 6, ClientCode = "CKDCS004", BranchName = "Golden Avenue" };
        public static ClientMaster CKD_BHAGTANWALA = new ClientMaster { Id = 7, ClientCode = "CKDCS005", BranchName = "Bhagtanwala" };
        public static ClientMaster CKD_PRAGDAS = new ClientMaster { Id = 8, ClientCode = "CKDCS006", BranchName = "Pragdas" };
        public static ClientMaster CKD_SULTANWIND_LINK_ROAD = new ClientMaster { Id = 9, ClientCode = "CKDCS007", BranchName = "Sultanwind Link Road" };
        public static ClientMaster CKD_FRIENDS_AVENUE = new ClientMaster { Id = 10, ClientCode = "CKDCS008", BranchName = "Friends Avenue" };
        public static ClientMaster CKD_RANJIT_AVENUE = new ClientMaster { Id = 11, ClientCode = "CKDCS009", BranchName = "Ranjit Avenue" };
        public static ClientMaster CKD_TARNTARAN = new ClientMaster { Id = 12, ClientCode = "CKDCS010", BranchName = "TarnTaran" };
        public static ClientMaster CKD_PATTI = new ClientMaster { Id = 13, ClientCode = "CKDCS011", BranchName = "Patti" };
        public static ClientMaster CKD_SURSINGH = new ClientMaster { Id = 14, ClientCode = "CKDCS012", BranchName = "Sursingh" };
        public static ClientMaster CKD_AJNALA = new ClientMaster { Id = 15, ClientCode = "CKDCS013", BranchName = "Ajnala" };
        public static ClientMaster CKD_ABDAL = new ClientMaster { Id = 16, ClientCode = "CKDCS014", BranchName = "Abdal" };
        public static ClientMaster CKD_MEHAL_JANDIALA = new ClientMaster { Id = 17, ClientCode = "CKDCS015", BranchName = "Mehal Jandiala" };
        public static ClientMaster CKD_NATHUPURA = new ClientMaster { Id = 18, ClientCode = "CKDCS016", BranchName = "Nathupura" };
        public static ClientMaster CKD_RASULPUR_KALAN = new ClientMaster { Id = 19, ClientCode = "CKDCS017", BranchName = "Rasulpur Kalan" };
        public static ClientMaster CKD_SEHNSRA = new ClientMaster { Id = 20, ClientCode = "CKDCS018", BranchName = "Sehnsra" };
        public static ClientMaster CKD_BURJ_MARHANA = new ClientMaster { Id = 21, ClientCode = "CKDCS019", BranchName = "Burj Marhana" };
        public static ClientMaster CKD_GHASEETPUR = new ClientMaster { Id = 22, ClientCode = "CKDCS020", BranchName = "Ghaseetpur" };
        public static ClientMaster CKD_ASAL_AUTAR = new ClientMaster { Id = 23, ClientCode = "CKDCS021", BranchName = "Asal Autar" };
        public static ClientMaster CKD_CHABHAL = new ClientMaster { Id = 24, ClientCode = "CKDCS022", BranchName = "Chabhal" };
        public static ClientMaster CKD_NAUSHERA_DHALLA = new ClientMaster { Id = 25, ClientCode = "CKDCS023", BranchName = "Naushera Dhalla" };
        public static ClientMaster CKD_KASEL = new ClientMaster { Id = 26, ClientCode = "CKDCS024", BranchName = "Kasel" };
        public static ClientMaster CKD_NAWANPIND = new ClientMaster { Id = 27, ClientCode = "CKDCS025", BranchName = "Nawan Pind" };
        public static ClientMaster CKD_ATTARI = new ClientMaster { Id = 28, ClientCode = "CKDCS026", BranchName = "Attari" };
        public static ClientMaster CKD_DHANOA_KALAN = new ClientMaster { Id = 29, ClientCode = "CKDCS027", BranchName = "Dhanoa Kalan" };
        public static ClientMaster CKD_MAJHWIND = new ClientMaster { Id = 30, ClientCode = "CKDCS028", BranchName = "Majhwind" };
        public static ClientMaster CKD_PIDDI = new ClientMaster { Id = 31, ClientCode = "CKDCS029", BranchName = "Piddi" };
        public static ClientMaster CKD_SRI_HARGOBINDPUR_SAHIB = new ClientMaster { Id = 32, ClientCode = "CKDCS030", BranchName = "Sri Hargobindpur Sahib" };
        public static ClientMaster CKD_NASERKE = new ClientMaster { Id = 33, ClientCode = "CKDCS031", BranchName = "Naserke" };
        public static ClientMaster CKD_MODEL_TOWN_HOSHIARPUR = new ClientMaster { Id = 34, ClientCode = "CKDCS032", BranchName = "Model Town, Hoshiarpur" };
        public static ClientMaster CKD_PANDORI_KHAZOOR = new ClientMaster { Id = 35, ClientCode = "CKDCS033", BranchName = "Pandori Khazoor" };
        public static ClientMaster CKD_ANANDPUR_SAHIB = new ClientMaster { Id = 36, ClientCode = "CKDCS034", BranchName = "Anandpur Sahib" };
        public static ClientMaster CKD_KHALSA_SR_SEC_SCHOOL = new ClientMaster { Id = 37, ClientCode = "CKDCS035", BranchName = "Khalsa Sr. Sec. School" };
        public static ClientMaster CKD_KIRATPUR_SAHIB = new ClientMaster { Id = 38, ClientCode = "CKDCS036", BranchName = "Kiratpur Sahib" };
        public static ClientMaster CKD_CHANDIGARH = new ClientMaster { Id = 39, ClientCode = "CKDCS037", BranchName = "Chandigarh" };
        public static ClientMaster CKD_LUDHIANA = new ClientMaster { Id = 40, ClientCode = "CKDCS038", BranchName = "Ludhiana" };
        public static ClientMaster CKD_DESA_SINGH_MAJITHIA = new ClientMaster { Id = 41, ClientCode = "CKDCS039", BranchName = "Desa Singh Majithia" };
        public static ClientMaster CKD_KURALI = new ClientMaster { Id = 42, ClientCode = "CKDCS040", BranchName = "Kurali" };
        public static ClientMaster CKD_CKDIMT_AMRITSAR = new ClientMaster { Id = 43, ClientCode = "CKDCS041", BranchName = "CKDIMT, Asr" };
        public static ClientMaster CKD_NURSING_COLLEGE = new ClientMaster { Id = 44, ClientCode = "CKDCS042", BranchName = "Nursing College" };
        public static ClientMaster CKD_ADARSH_SCHOOL_DHANDRA = new ClientMaster { Id = 45, ClientCode = "CKDCS043", BranchName = "Adarsh School, Dhandra" };
        public static ClientMaster CKD_ADARSH_SCHOOL_NAUSHEHRA_PANNUAN = new ClientMaster { Id = 46, ClientCode = "CKDCS044", BranchName = "Adarsh School, Naushehra Pannuan" };
        public static ClientMaster CKD_CENTER_OF_CIVIL_STUDIES = new ClientMaster { Id = 47, ClientCode = "CKDCS045", BranchName = "Centre of Civil Services Studies" };
        public static ClientMaster CKD_KAPURTHALA = new ClientMaster { Id = 48, ClientCode = "CKDCS046", BranchName = "Kapurthala" };
        public static ClientMaster CKD_CENTRAL_KHALSA_ORPHANAGE = new ClientMaster { Id = 63, ClientCode = "CKDCS049", BranchName = "Central Khalsa Orphanage" };
        public static ClientMaster CKD_HOSPITAL = new ClientMaster { Id = 61, ClientCode = "CKDCS051", BranchName = "Hospital" };
        public static ClientMaster CKD_CENTRAL_KHALSA_HOSPITAL = new ClientMaster { Id = 1179, ClientCode = "CKDCS053", BranchName = "Central Khalsa Hospital" };
        public static ClientMaster CKD_KANPUR = new ClientMaster { Id = 1178, ClientCode = "CKDCS055", BranchName = "Kanpur" };
        public static ClientMaster CKD_IMT_TARNTARAN = new ClientMaster { Id = 62, ClientCode = "CKDCS056", BranchName = "CKDIMT, Tarntaran" };
        public static ClientMaster CKD_NANDA_CHAUR = new ClientMaster { Id = 64, ClientCode = "CKDCS057", BranchName = "Nanda Chaur" };
        public static ClientMaster CKD_LOCAL_COMMITTEE_LUDHIANA = new ClientMaster { Id = 1177, ClientCode = "CKDCS059", BranchName = "Local Committee, Ldh" };
        public static ClientMaster CKD_HEAD_OFFICE = new ClientMaster { Id = 177, ClientCode = "CKDCS068", BranchName = "Head Office" };
        public static ClientMaster CKD_UCHA_PIND = new ClientMaster { Id = 1180, ClientCode = "CKDCS070", BranchName = "Ucha Pind" };
        public static ClientMaster CKD_SHUBHAB_ENCLAVE_ = new ClientMaster { Id = 2196, ClientCode = "CKDCS073", BranchName = "Shubham Enclave" };





        public static ClientMaster SIDANA_INTERNATIONAL_SCHOOL_2 = new ClientMaster { Id = 173, ClientCode = "SDNIS", BranchName = "Sidana International School2" };
        public static ClientMaster SIDANA_INTERNATIONAL_SCHOOL = new ClientMaster { Id = 175, ClientCode = "SDNINS", BranchName = "Sidana International School" };
        public static ClientMaster SIDANA_POLYTECHNIC_COLLEGE = new ClientMaster { Id = 172, ClientCode = "SDNPTC", BranchName = "Sidana Polytechnic College" };
        public static ClientMaster SIDANA_INSTITUTE_OF_EDUCATION = new ClientMaster { Id = 171, ClientCode = "SDNIE", BranchName = "Sidana Institute of Education" };
        public static ClientMaster SIDANA_DEGREE_COLLEGE = new ClientMaster { Id = 170, ClientCode = "SDNDC", BranchName = "Sidana Degree College" };





        public static ClientMaster BRS_31_B_PADAMPUR = new ClientMaster { Id = 149, ClientCode = "GNPDP", BranchName = "31 BB PADAMPUR" };
        public static ClientMaster BRS_BAGHA = new ClientMaster { Id = 139, ClientCode = "BTBGA", BranchName = "BAGHA" };
        public static ClientMaster BRS_BAKAR_WALA = new ClientMaster { Id = 111, ClientCode = "MOBKW", BranchName = "BAKAR WALA" };
        public static ClientMaster BRS_BALBEHRA = new ClientMaster { Id = 116, ClientCode = "PTBAL", BranchName = "BALBEHRA" };
        public static ClientMaster BRS_BANGI_NIHAL_SINGH = new ClientMaster { Id = 140, ClientCode = "BTBNS", BranchName = "BANGI NIHAL SINGH" };
        public static ClientMaster BRS_BARAGURA = new ClientMaster { Id = 153, ClientCode = "SSBRG", BranchName = "BARAGURA" };
        public static ClientMaster BRS_BARU_SAHIB_EM = new ClientMaster { Id = 163, ClientCode = "SMAAB", BranchName = "BARU SAHIB (E.M)" };
        public static ClientMaster BRS_BASERKE = new ClientMaster { Id = 58, ClientCode = "ARBSK", BranchName = "BASARKE" };
        public static ClientMaster BRS_BASTI_BASAWA_SINGH = new ClientMaster { Id = 56, ClientCode = "FZBVS", BranchName = "BASTI BASAWA SINGH" };
        public static ClientMaster BRS_BEHAK_FATTU = new ClientMaster { Id = 55, ClientCode = "FZBHF", BranchName = "BEHAK FATTU" };
        public static ClientMaster BRS_BENRA = new ClientMaster { Id = 123, ClientCode = "SRBNR", BranchName = "BENRA" };
        public static ClientMaster BRS_BHAI_DESA = new ClientMaster { Id = 130, ClientCode = "MNBHD", BranchName = "BHAI DESA" };
        public static ClientMaster BRS_BHADAUR = new ClientMaster { Id = 84, ClientCode = "BRBDR", BranchName = "BHADAUR" };
        public static ClientMaster BRS_BHARANA = new ClientMaster { Id = 54, ClientCode = "FZBHR", BranchName = "BHARANA" };
        public static ClientMaster BRS_BARYAL_LEHRI = new ClientMaster { Id = 86, ClientCode = "GPBHL", BranchName = "BHRYAL LAHRI" };
        public static ClientMaster BRS_BHUNDER_BHAINI = new ClientMaster { Id = 127, ClientCode = "SRBNB", BranchName = "BHUNDER BHAINI" };
        public static ClientMaster BRS_BHUNSLA = new ClientMaster { Id = 159, ClientCode = "KTBNS", BranchName = "BHUNSLA" };
        public static ClientMaster BRS_BOPARAI_KALAN = new ClientMaster { Id = 97, ClientCode = "JLBPK", BranchName = "BOPARAI KALAN" };
        public static ClientMaster BRS_CHAHAL_KALAN = new ClientMaster { Id = 99, ClientCode = "NSCHL", BranchName = "CHAHAL KALAN" };
        public static ClientMaster BRS_CHAK_BHAI_KE = new ClientMaster { Id = 136, ClientCode = "MNCBK", BranchName = "CHAK BAI KE" };
        public static ClientMaster BRS_CHAK_DES_RAJ = new ClientMaster { Id = 70, ClientCode = "JLCDR", BranchName = "CHAK DES RAJ" };
        public static ClientMaster BRS_CHAK_MANDER = new ClientMaster { Id = 100, ClientCode = "NSCHM", BranchName = "CHAK MANDER" };
        public static ClientMaster BRS_CHOGAWAN = new ClientMaster { Id = 51, ClientCode = "ARCHG", BranchName = "CHOGAWAN" };
        public static ClientMaster BRS_CHOLANG = new ClientMaster { Id = 98, ClientCode = "JLCLG", BranchName = "CHOLANG" };
        public static ClientMaster BRS_CHUNNI_KALAN = new ClientMaster { Id = 78, ClientCode = "MOCHN", BranchName = "CHUNNI KALAN" };
        public static ClientMaster BRS_DADEHAR_SAHIB = new ClientMaster { Id = 52, ClientCode = "TTDDS", BranchName = "DADEHAR SAHIB" };
        public static ClientMaster BRS_DAKRA_SAHIB = new ClientMaster { Id = 161, ClientCode = "PKDKS", BranchName = "DAKRA SAHIB" };
        public static ClientMaster BRS_DAMDAMA_SAHIB = new ClientMaster { Id = 82, ClientCode = "BTDDS", BranchName = "DAMDAMA SAHIB" };
        public static ClientMaster BRS_DAREWALA = new ClientMaster { Id = 151, ClientCode = "SSDRW", BranchName = "DAREWALA" };
        public static ClientMaster BRS_DASHMESH_NAGAR = new ClientMaster { Id = 158, ClientCode = "FBDSN", BranchName = "DASHMESH NAGAR" };
        public static ClientMaster BRS_DAULA = new ClientMaster { Id = 143, ClientCode = "MSDLA", BranchName = "DAULA" };
        public static ClientMaster BRS_DAYALPUR_MIRZA = new ClientMaster { Id = 142, ClientCode = "BTDPM", BranchName = "DAYALPUR MIRZA" };
        public static ClientMaster BRS_DHALIWAL_BAIT = new ClientMaster { Id = 91, ClientCode = "KTDLB", BranchName = "DHALIWAL BAIT" };
        public static ClientMaster BRS_DHAMOT = new ClientMaster { Id = 80, ClientCode = "LDDMT", BranchName = "DHAMOT" };
        public static ClientMaster DHANAL_KALAN = new ClientMaster { Id = 69, ClientCode = "JLDNK", BranchName = "DHANAL KALAN" };
        public static ClientMaster BRS_DHARAMGHAR_CHHANNA = new ClientMaster { Id = 126, ClientCode = "SRDGC", BranchName = "DHARAMGHAR CHHANNA" };
        public static ClientMaster BRS_DHINDSA = new ClientMaster { Id = 73, ClientCode = "LDDND", BranchName = "DHINDSA" };
        public static ClientMaster BRS_DHOTIAN = new ClientMaster { Id = 60, ClientCode = "TTDHT", BranchName = "DHOTIAN" };
        public static ClientMaster BRS_DHUDUAL_KHALSA = new ClientMaster { Id = 114, ClientCode = "PTDUD", BranchName = "DHUDIAL KHALSA" };
        public static ClientMaster BRS_DHUGGA_KALAN = new ClientMaster { Id = 68, ClientCode = "HSDUG", BranchName = "DHUGGA KALAN" };
        public static ClientMaster BRS_ELLANABAD = new ClientMaster { Id = 152, ClientCode = "SSELB", BranchName = "ELLANABAD" };
        public static ClientMaster BRS_FG_CHANNA = new ClientMaster { Id = 117, ClientCode = "PTFCH", BranchName = "FG. CHANNA" };
        public static ClientMaster BRS_FG_GANDUAN = new ClientMaster { Id = 121, ClientCode = "SRFGG", BranchName = "FG. GANDUAN" };
        public static ClientMaster BRS_GANGANAGAR = new ClientMaster { Id = 79, ClientCode = "GNGNG", BranchName = "GANGANAGAR" };
        public static ClientMaster BRS_GHUGH = new ClientMaster { Id = 72, ClientCode = "JLGUG", BranchName = "GHUGH" };
        public static ClientMaster BRS_GOBINDPUR = new ClientMaster { Id = 101, ClientCode = "NSGBP", BranchName = "GOBINDPUR" };
        public static ClientMaster BRS_GOMTI = new ClientMaster { Id = 164, ClientCode = "PIGMT", BranchName = "GOMTI (E.M)" };
        public static ClientMaster BRS_HABRI = new ClientMaster { Id = 162, ClientCode = "KKHBR", BranchName = "HABRI" };
        public static ClientMaster BRS_HOLI_BARA = new ClientMaster { Id = 167, ClientCode = "AMHBR", BranchName = "HOLI BARA" };
        public static ClientMaster BRS_JAGA_RAM_TIRATH = new ClientMaster { Id = 66, ClientCode = "BTJRT", BranchName = "JAGA RAM TIRATH" };
        public static ClientMaster BRS_JAND_SAHIB = new ClientMaster { Id = 104, ClientCode = "FRJNS", BranchName = "JAND SAHIB" };
        public static ClientMaster BRS_JANDIALI = new ClientMaster { Id = 65, ClientCode = "LDJND", BranchName = "Jandiali" };
        public static ClientMaster BRS_JAWAHAR_KE = new ClientMaster { Id = 138, ClientCode = "MNJWK", BranchName = "JAWAHAR KE" };
        public static ClientMaster BRS_JHANDIANA = new ClientMaster { Id = 106, ClientCode = "MOJHN", BranchName = "JHANDIANA" };
        public static ClientMaster BRS_KAJRI = new ClientMaster { Id = 165, ClientCode = "PIKJR", BranchName = "KAJRI" };
        public static ClientMaster BRS_KAKRA_KALAN = new ClientMaster { Id = 71, ClientCode = "JLKRK", BranchName = "KAKRA KALAN" };
        public static ClientMaster BRS_KALEKE = new ClientMaster { Id = 110, ClientCode = "MOKLK", BranchName = "KALEKE" };
        public static ClientMaster BRS_KALHON = new ClientMaster { Id = 83, ClientCode = "MNKLO", BranchName = "KALHON" };
        public static ClientMaster BRS_KAMALPUR = new ClientMaster { Id = 77, ClientCode = "ROKML", BranchName = "KAMALPUR" };
        public static ClientMaster BRS_KAMRANI = new ClientMaster { Id = 150, ClientCode = "HGKAM", BranchName = "KAMRANI" };
        public static ClientMaster BRS_KHAMMNOO = new ClientMaster { Id = 76, ClientCode = "FSKHM", BranchName = "KHAMMNOO" };
        public static ClientMaster BRS_KHERA = new ClientMaster { Id = 95, ClientCode = "HSKHR", BranchName = "KHERA" };
        public static ClientMaster BRS_KHICHIPUR = new ClientMaster { Id = 169, ClientCode = "JLKCP", BranchName = "KHICHIPUR" };
        public static ClientMaster BRS_KHOKHAR = new ClientMaster { Id = 155, ClientCode = "SSKHK", BranchName = "KHOKHAR" };
        public static ClientMaster BRS_KHUHIAN_SERVER = new ClientMaster { Id = 147, ClientCode = "FZKHS", BranchName = "KHUHIAN SERVER" };
        public static ClientMaster BRS_KILLI_NIHAL_SINGH = new ClientMaster { Id = 141, ClientCode = "BTKNS", BranchName = "KILLI NIHAL SINGH" };
        public static ClientMaster BRS_KOLLOANWALI = new ClientMaster { Id = 146, ClientCode = "MSKOL", BranchName = "KOLLIANWALI" };
        public static ClientMaster BRS_KURIWARA = new ClientMaster { Id = 133, ClientCode = "MNKWR", BranchName = "KURIWARA" };
        public static ClientMaster BRS_KUSLA = new ClientMaster { Id = 137, ClientCode = "MNKSL", BranchName = "KUSLA" };
        public static ClientMaster BRS_MADHIR = new ClientMaster { Id = 144, ClientCode = "MSMDR", BranchName = "MADHIR" };
        public static ClientMaster BRS_MADHOPUR = new ClientMaster { Id = 75, ClientCode = "FSMDP", BranchName = "MADHORPUR" };
        public static ClientMaster BRS_MAJRI = new ClientMaster { Id = 160, ClientCode = "AMMJR", BranchName = "MAJRI" };
        public static ClientMaster BRS_MAKHANGARH = new ClientMaster { Id = 96, ClientCode = "HSMKN", BranchName = "MAKHANGARH" };
        public static ClientMaster BRS_MANAL = new ClientMaster { Id = 124, ClientCode = "SRMNL", BranchName = "MANAL" };
        public static ClientMaster BRS_MANAWAN = new ClientMaster { Id = 107, ClientCode = "MOMNW", BranchName = "MANAWAN" };
        public static ClientMaster BRS_MANDER = new ClientMaster { Id = 135, ClientCode = "MNMDR", BranchName = "MANDER" };
        public static ClientMaster BRS_MANDER_DONA = new ClientMaster { Id = 90, ClientCode = "KTMDD", BranchName = "MANDER DONA" };
        public static ClientMaster BRS_MANOLI_SURAT = new ClientMaster { Id = 113, ClientCode = "ROMNS", BranchName = "MANOLI SURAT" };
        public static ClientMaster BRS_MAYO_PATTI = new ClientMaster { Id = 92, ClientCode = "KTMYP", BranchName = "MAYO PATTI" };
        public static ClientMaster BRS_MEHAL_KALAN = new ClientMaster { Id = 128, ClientCode = "BRMHK", BranchName = "MEHAL KALAN" };
        public static ClientMaster BRS_MOOLIAN_WAL = new ClientMaster { Id = 88, ClientCode = "GPMLW", BranchName = "MOOLIAN WAL" };
        public static ClientMaster BRS_MUKTSAR = new ClientMaster { Id = 145, ClientCode = "MSMKT", BranchName = "MUKTSAR" };
        public static ClientMaster BRS_NAWAN_QILA = new ClientMaster { Id = 103, ClientCode = "FZNWQ", BranchName = "NAWAN QILA" };
        public static ClientMaster BRS_PHAPHERE_BHAI_KE = new ClientMaster { Id = 131, ClientCode = "MNPBK", BranchName = "PHAPHERE BHAI KE" };
        public static ClientMaster BRS_PURANEWALA = new ClientMaster { Id = 109, ClientCode = "MOPNW", BranchName = "PURANEWALA" };
        public static ClientMaster BRS_PEER_BUX_WALA = new ClientMaster { Id = 89, ClientCode = "KTRPB", BranchName = "R. PEER BUX WALA" };
        public static ClientMaster BRS_RACCHIN = new ClientMaster { Id = 112, ClientCode = "LDRCH", BranchName = "RACCHIN" };
        public static ClientMaster BRS_RAJIA = new ClientMaster { Id = 129, ClientCode = "BRRJI", BranchName = "RAJIA" };
        public static ClientMaster BRS_RAM_PUR_SONDA = new ClientMaster { Id = 93, ClientCode = "KTRPS", BranchName = "RAM PUR SONDA" };
        public static ClientMaster BRS_RAM_SINGH_PURA = new ClientMaster { Id = 81, ClientCode = "GNRSP", BranchName = "RAM SINGH PURA" };
        public static ClientMaster BRS_RAMPUR_NAROTAMPUR = new ClientMaster { Id = 59, ClientCode = "TTRNP", BranchName = "RAMPUR NAROTAMPUR" };
        public static ClientMaster BRS_RANNO = new ClientMaster { Id = 118, ClientCode = "PTRNO", BranchName = "RANNO" };
        public static ClientMaster BRS_RASULPUR_BAIT = new ClientMaster { Id = 67, ClientCode = "GPRSL", BranchName = "RASULPUR BAIT" };
        public static ClientMaster BRS_RATIA = new ClientMaster { Id = 157, ClientCode = "FBRAT", BranchName = "RATIA" };
        public static ClientMaster BRS_RATTA_KHERA = new ClientMaster { Id = 102, ClientCode = "FZRTK", BranchName = "RATTA KHERA" };
        public static ClientMaster BRS_RATTALON = new ClientMaster { Id = 120, ClientCode = "SRRTL", BranchName = "RATTALON" };
        public static ClientMaster BRS_RATTIAN = new ClientMaster { Id = 108, ClientCode = "MORAT", BranchName = "RATTIAN" };
        public static ClientMaster BRS_REETHKHERI = new ClientMaster { Id = 115, ClientCode = "PTRTH", BranchName = "REETHKHERI" };
        public static ClientMaster BRS_RORI = new ClientMaster { Id = 154, ClientCode = "SSROR", BranchName = "RORI" };
        public static ClientMaster BRS_SACHA_SAUDA = new ClientMaster { Id = 168, ClientCode = "JNSSD", BranchName = "SACHA SAUDA" };
        public static ClientMaster BRS_SAIDOWAL = new ClientMaster { Id = 94, ClientCode = "KTSDW", BranchName = "SAIDOWAL" };
        public static ClientMaster BRS_SALAMKHERA = new ClientMaster { Id = 156, ClientCode = "FBSMK", BranchName = "SALAMKHERA" };
        public static ClientMaster BRS_SANGHA = new ClientMaster { Id = 134, ClientCode = "MNSNG", BranchName = "SANGHA" };
        public static ClientMaster BRS_SANYANA = new ClientMaster { Id = 166, ClientCode = "FBSNA", BranchName = "SANYANA" };
        public static ClientMaster BRS_SEONA = new ClientMaster { Id = 119, ClientCode = "PTSNA", BranchName = "SEONA" };
        public static ClientMaster BRS_SHERON = new ClientMaster { Id = 125, ClientCode = "SRSHR", BranchName = "SHERON" };
        public static ClientMaster BRS_SHERON_BAGHHA = new ClientMaster { Id = 57, ClientCode = "ARSBH", BranchName = "SHERON BAGHHA" };
        public static ClientMaster BRS_SOWADDI_KALAN = new ClientMaster { Id = 74, ClientCode = "LDSWK", BranchName = "SOWADDI KALAN" };
        public static ClientMaster BRS_SUJANPUR = new ClientMaster { Id = 85, ClientCode = "GPSJN", BranchName = "SUJANPUR" };
        public static ClientMaster BRS_SUKKAN_WALA = new ClientMaster { Id = 105, ClientCode = "FRSKW", BranchName = "SUKKAN WALA" };
        public static ClientMaster BRS_TEJA_SINGH_WALA = new ClientMaster { Id = 53, ClientCode = "TTTSW", BranchName = "TEJA SINGH WALA" };
        public static ClientMaster BRS_TELIPURA = new ClientMaster { Id = 174, ClientCode = "RPTLP", BranchName = "TELIPURA" };
        public static ClientMaster BRS_THEH_KALANDER = new ClientMaster { Id = 148, ClientCode = "FZTHK", BranchName = "THEH KALANDER" };
        public static ClientMaster BRS_TIBBER = new ClientMaster { Id = 87, ClientCode = "GPTBR", BranchName = "TIBBER" };
        public static ClientMaster BRS_UBHA = new ClientMaster { Id = 122, ClientCode = "SRUBI", BranchName = "UBHA" };
        public static ClientMaster BRS_UDDAT_SAIDEWALA = new ClientMaster { Id = 132, ClientCode = "MNUSW", BranchName = "UDDAT SAIDEWALA" };
        public static ClientMaster BRS_VACHHOYA = new ClientMaster { Id = 50, ClientCode = "ARVCH", BranchName = "VACHHOYA" };
        public static ClientMaster BRS_AKAL_WORKSHOP_BHIKHI = new ClientMaster { Id = 1183, ClientCode = "BS003", BranchName = "Akal Workshop Bhikhi" };
        public static ClientMaster BRS_AUDIT_OFFICE_CHUNNI_KALAN = new ClientMaster { Id = 1184, ClientCode = "BS004", BranchName = "Audit Office Chunni Kalan" };
        public static ClientMaster BRS_CIVIL_OFFICE_DHOTIAN = new ClientMaster { Id = 1185, ClientCode = "BS005", BranchName = "Civil Office Dhotian" };
        public static ClientMaster BRS_CIVIL_OFFICE_MANAWAN = new ClientMaster { Id = 1186, ClientCode = "BS006", BranchName = "Civil Office Manawan" };
        public static ClientMaster BRS_CIVIL_OFFICE_CHAK_MANDER = new ClientMaster { Id = 1187, ClientCode = "BS007", BranchName = "Civil Office Chak Mander" };
        public static ClientMaster BRS_CIVIL_OFFICE_TIBBER = new ClientMaster { Id = 1188, ClientCode = "BS008", BranchName = "Civil Office Tibber" };
        public static ClientMaster BRS_AUDIT_OFFICE_JANDIALI = new ClientMaster { Id = 1189, ClientCode = "BS009", BranchName = "Audit Office Jandiali" };
        public static ClientMaster BRS_PATIALA_DUDIAL_KHALSA = new ClientMaster { Id = 1190, ClientCode = "BS010", BranchName = "Patiala Dudial Khalsa" };
        public static ClientMaster BRS_SABO_KI_TALWANDI = new ClientMaster { Id = 1191, ClientCode = "BS011", BranchName = "Sabo Ki Talwandi" };
        public static ClientMaster BRS_MOGA_OFFICE = new ClientMaster { Id = 1181, ClientCode = "BS001", BranchName = "Moga Office" };
        public static ClientMaster BRS_UNIFORM_CELL_CHUNNI = new ClientMaster { Id = 1182, ClientCode = "BS002", BranchName = "Uniform Cell Chunni" };
        public static ClientMaster BRS_CIVIL_ZONE_DHANAL_KALAN = new ClientMaster { Id = 2193, ClientCode = "BS012", BranchName = "Civil Zone Dhanal Kalan" };
        public static ClientMaster BRS_RELIGIONAL_OFFICE_SRI_MUKTSAR_SAHIB = new ClientMaster { Id = 3196, ClientCode = "BS013", BranchName = "Regional Office (Sri Muktsar Sahib)" };
        public static ClientMaster BRS_CIVIL_ZONE_MEHAL_KALAN = new ClientMaster { Id = 3201, ClientCode = "BS015", BranchName = "Mehal Kalan Civil Zone" };
        public static ClientMaster BRS_GURDWARA_SAHIB_DHINDSA = new ClientMaster { Id = 3202, ClientCode = "BS016", BranchName = "Gurdwara Sahib Dhindsa" };
        public static ClientMaster BRS_CIVIL_STAFF_JAGA_RAM_ZONE = new ClientMaster { Id = 3199, ClientCode = "BS014", BranchName = "Civil Staff Jagaram Zone" };





        public static ClientMaster LOTUS_VALLEY = new ClientMaster { Id = 176, ClientCode = "LOTUS", BranchName = "LOTUS VALLEY PUBLIC SCHOOL" };





        public static ClientMaster ST_WARRIORS_SCHOOL = new ClientMaster { Id = 2195, ClientCode = "STWPS", BranchName = "St. Warriors School" };





        public static ClientMaster SHAH_HARBANS_SINGH_INTERNATIONAL_PUBLIC_SCHOOL = new ClientMaster { Id = 1192, ClientCode = "SHRBNS", BranchName = "Shah Harbans Singh International Public School" };





        public static ClientMaster MG_CONVENT_SCHOOL = new ClientMaster { Id = 3198, ClientCode = "MGJCS", BranchName = "Mata Gujri Convent School" };






        public static ClientMaster HOWARD_LANE_SENIOR_SCHOOL = new ClientMaster { Id = 3203, ClientCode = "HOWLS", BranchName = "HOWARD LANE SENIOR SCHOOL" };





        public static ClientMaster SAROOP_RANI_GOVT_COLLEGE_AMRITSAR = new ClientMaster { Id = 3200, ClientCode = "SRGCW", BranchName = "Saroop Rani Govt. College for Women"};





        public static ClientMaster MGS_MEMORIAL_ANTERYAMI_MODEL_SCHOOL  = new ClientMaster { Id = 3204, ClientCode = "MGSMS", BranchName = "M.G.S.Memorial Anteryami Model School" };





        public static ClientMaster GOGNA_BUS_SERVICE = new ClientMaster { Id = 3205, ClientCode = "GGNBS", BranchName = "GOGNA BUS SERVICE" };
    }
}
