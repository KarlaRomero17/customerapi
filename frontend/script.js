// var tabla = document.getElementById('customers');

var customers = [];
document.addEventListener("DOMContentLoaded", init);
const api_url = 'https://localhost:7142/api/customer/';
// const api_url = 'https://localhost:7273/api/customer/';

function init() {
    search()
}
function abrirFormulario(){
   // htmlModal= document.getElementById('modal_edit');
   // htmlModal.setAttribute("class","modale opened");
   $("#myModal").modal();

}
/*function cerrarFormulario(){
    htmlModal= document.getElementById('modal_edit');
    htmlModal.setAttribute("class","modale");
}*/

async function search() {

    var url = api_url + 'getcustomers'
    var response = await fetch(url, {
        'method': 'GET',
        'headers': {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "application/json"
        }
    });

    customers = await response.json();
    console.log(customers);

    var html = '';
for (customer of customers){
   // debugger;
    var row = `<tr>
    <td>${customer.firstName}</td>
    <td>${customer.lastName}</td>
    <td>${customer.email}</td>
    <td>${customer.phone}</td>
    <td>
        <button onclick="edit(${customer.id})" class="btneditar" >Editar</button>
        <button onclick="remove(${customer.id})" class="btnEliminar">Eliminar</button>
    </td>
</tr>`;
html = html +row;
}   

    document.querySelector('#customers > tbody').outerHTML = html;
}

async function remove(id){
    respuesta = confirm('Esta seguro de eliminar?');
    if(respuesta){
        
        var url = api_url + 'deletecustomer/' +id
        await fetch(url, {
            'method': 'DELETE',
            'headers': {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json"
            }
        })
        window.location.reload();
    }
}

async function save(){
    var data ={
        "firstName": document.getElementById('nombre').value,
        "lastName": document.getElementById('apellido').value,
        "email": document.getElementById('email').value,
        "phone": document.getElementById('telefono').value,
        "address": document.getElementById('direccion').value
      };
      var id =  document.getElementById('id').value
      if(id !=''){
          data.id =id
      }
      var action = id != '' ? 'updatecustomer' : 'createcustomer'
      var url = api_url + action 
        
        await fetch(url, {
            'method': id != '' ? 'PUT' : 'POST',
            'body': JSON.stringify(data),
            'headers': {
                "Access-Control-Allow-Origin": "*",
                "Content-Type": "application/json"
            }
        })
       // document.location.href = "index.html";
            
}

function edit(id){

    var customer = customers.find(x => x.id ==id)
    document.getElementById('nombre').value = customer.firstName
    document.getElementById('apellido').value = customer.lastName
    document.getElementById('email').value = customer.email
    document.getElementById('telefono').value = customer.phone
    document.getElementById('direccion').value = customer.address
    document.getElementById('id').value = customer.id
    abrirFormulario();
    //console.log(customer)
}