package Model;

import java.util.ArrayList;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface DataService {

    @POST("mon/test")
    Call<Void> test(@Body ArrayList<Integer> a);

}
