import './Table.css';
import React,{useState} from 'react';
import axios from 'axios';

function Table(){
  const [formValues,setFormValues] = useState([{DateRegistration:"",DateLastActivity:""}])

    let handleChange = (i, e) => {
      let newFormValues = [...formValues];
      newFormValues[i][e.target.name] = e.target.value;
      setFormValues(newFormValues);
    }

  let addFormFields = () => {
      setFormValues([...formValues, { DateRegistration: "", DateLastActivity: "" }])
    }

  let removeFormFields = (i) => {
      let newFormValues = [...formValues];
      newFormValues.splice(i, 1);
      setFormValues(newFormValues)
  }

  let handleSubmit = (event) => {
      event.preventDefault();
      
     axios.post ('https://localhost:44337/api/Users/AddRange', formValues)
     .then((responce)=>{

       if(responce.status ===200){         
         console.log("SUCCESSS")
         alert("SUCCESSS");
       }else{
          console.log("SOMETHING WENT WRONG")
         alert("WRONG");

       }

     });
  }

  return (
      <form  id="MyForm" onSubmit={handleSubmit}>
        <div className="form-header">
          <p id="form-id" >id</p>
          <p id="form-date-regist">Date Registration</p>
          <p id="form-date-lastAct">Date Last Activity</p>    
        </div>

      {formValues.map((element, index) => (
        <div className="form-body" key={index}>
          

          <input className="inputDateReg"  type="date" name="DateRegistration" value={element.DateRegistration || ""} onChange={e => handleChange(index, e)} />      
          <input className="inputDateLast" type="date" name="DateLastActivity" value={element.DateLastActivity || ""} onChange={e => handleChange(index, e)} />
         
          {
            index ? 
              <button type="button"  className="btnRemove" onClick={() => removeFormFields(index)}>❌</button> 
            : null
          }
        </div>
      ))}
      <div className="button-section">
          <button className="btnAdd" type="button" onClick={() => addFormFields()}> ➕ Add one more</button>
          <button className="btnSubmit" type="submit" >Save</button>
      </div>      
    </form>


  ) 
  
 
}


 
export default Table;
