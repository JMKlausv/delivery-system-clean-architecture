using Domain.Entities;

namespace Tests.Application.Tests.MockData;
public class ViechleMockData {
    public static List<Viechle> GetViechles(){
        return new List<Viechle>{
            new Viechle {
                
                Model = "model1",
                LicenceNumber = "DD438975983",  
                Type = "type1",    
            },
              new Viechle {
                
                Model = "model2",
                LicenceNumber = "hg7487398298",  
                Type = "type2",    
            },
              new Viechle {
                
                Model = "model3",
                LicenceNumber = "hg7487398287",  
                Type = "type2",    
            }
        };
    }
}