import React,{Component} from 'react';
import BarGraph from './BarGraph'
import axios from 'axios';

class Retention extends Component {
    constructor(props){
        super(props);
        this.state={
            days:{
                day:7
            },
            rollingRetention:{
                retention:''
            }
        };
    }
    


    onCalculate=(event)=>{
        event.preventDefault();
        axios.get('https://localhost:44337/api/Retention',{params:{days:this.state.days.day}})
        .then(responce=>{

            this.setState(()=>({
              rollingRetention:{
                  retention: responce.data
              }  
            }));

        });
    }

    onChangeDays=(e)=>{
        this.setState((state)=>({
            days:{
                day:e.target.value
            }
        }));
    }  
    render(){
        return (
            <div style={{margin:220}} >
                <input type="number" name="day" value={this.state.days.day} onChange={this.onChangeDays}/>
                <button onClick={this.onCalculate}>Calculate</button>
                <p>{this.state.rollingRetention.retention}</p>
               
                <BarGraph />
            </div>
        )
    }
}

export default Retention;
