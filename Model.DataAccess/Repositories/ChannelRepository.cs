
		 
	

    
using Model.DataAccess.DatabaseContext;
using Model.DataAccess.Models;								   
              
public class ChannelRepository : GenericRepository<Channel>
{   
    public ChannelRepository(ModelArchContext context) : base (context)
    {
      
    }

    //Override any generic method for your own custom implemention, add new repository methods to the IChannelRepository.cs file
}

