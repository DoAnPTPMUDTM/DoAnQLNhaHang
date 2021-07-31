package Model;

public class APIService {
    private static String base_url = "http://192.168.1.4:5000/api/";//"http://localhost:5000/api/";
    public  static DataService getService(){
        return APIRetrofitClient.getClient(base_url).create(DataService.class);
    }
}

