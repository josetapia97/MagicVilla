using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        private readonly IVillaRepositorio _villarepo;
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, IVillaRepositorio villarepo, IMapper mapper)
        {

            _logger = logger;
            _villarepo = villarepo;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las villas");
            IEnumerable<Villa> villalist = await _villarepo.ObtenerTodos();
            return Ok(_mapper.Map<IEnumerable<VillaDto>>(villalist));
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer villa con id "+ id);
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa =await _villarepo.Obtener(x => x.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _villarepo.Obtener(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }
            
            Villa modelo = _mapper.Map<Villa>(createDto);
            

            await _villarepo.Crear(modelo);
            return CreatedAtRoute("GetVilla", new {id = modelo.Id}, modelo);

        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var villa = await _villarepo.Obtener(v => v.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            _villarepo.Remover(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto) 
        {
            if(updateDto == null || id != updateDto.Id)  
            {
                return BadRequest();
            }


            Villa modelo = _mapper.Map<Villa>(updateDto);
            
            _villarepo.Actualizar(modelo);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var villa = await _villarepo.Obtener(x => x.Id == id,tracked:false);

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);


            if(villa==null) return BadRequest();

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa modelo = _mapper.Map<Villa>(villaDto);


            _villarepo.Actualizar(modelo);

            return NoContent();
        }

    }
}
