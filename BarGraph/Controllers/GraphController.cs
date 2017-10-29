
using BarGraph.DTO;
using BarGraph.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BarGraph.Controllers
{
    public class GraphController : ApiController
    {
        private readonly IGraphDataService graphDataService;
        private readonly IGraphFileDataReader graphFileDataReader;

        public GraphController(IGraphDataService graphDataService, IGraphFileDataReader graphFileDataReader)
        {
            this.graphDataService = graphDataService;
            this.graphFileDataReader = graphFileDataReader;
        }

        // GET: api/Chart
        public IEnumerable<GraphColumnDTO> Get()
        {
            return graphDataService.RandomizeValues().Select(d => new GraphColumnDTO(d));
        }
        
        // POST: api/Chart
        public async Task<IHttpActionResult> Post()
        {
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            
            using (var fileStream = await provider.Contents[0].ReadAsStreamAsync())
            {
                var fileContent = graphFileDataReader.GetLinesFromFile(fileStream);
                var data = graphFileDataReader.GetColumns(fileContent);
                var validFile = data.All(d => d.IsValid);

                if (validFile)
                {
                    graphDataService.SetInitialValues(data);
                }

                var responseDTO = data.Select(d => new GraphColumnDTO(d));

                return Ok(new
                {
                    validFile = validFile,
                    data = responseDTO
                });
            }
        }
        
    }
}
