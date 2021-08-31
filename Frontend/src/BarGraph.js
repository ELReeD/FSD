import React,{useEffect , useState} from 'react';
import axios from 'axios';
import { Line } from "react-chartjs-2";


//#region 
/*
class BarGraph extends Component{
    constructor(props){
        super(props);
        this.state={
            data:{
                //labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
                datasets: [
                    {
                      label: '#  User Lifecycle',
                      data: [],
                      borderColor:['rgba(255, 99, 132, 1)'] ,
                      borderWidth: 1,
                    },
                  ]
            },
            options:{
                scales: {
                    yAxes: [
                      {
                        ticks: {
                          beginAtZero: true,
                        },
                      },
                    ],
                }
            }              
        }
        this.onInit();
    }
    async onInit(){

        let NewDate = this.state.data.datasets[0];
        let arr =[];
        await axios.get('https://localhost:44337/api/Retention/LifeSpan')
            .then(responce=>{
                NewDate.data = responce.data;
                console.log(this.state);
            });




       /* .then(responce=>{
            arr = responce.data;          
            console.log("responce:" + responce.data);           
        }).then(()=>{
           NewDate.data = arr;
            this.setState(()=>({
                    data:{
                        datasets:NewDate
                    }
            }));
        });
*/
      /*  this.setState(()=>({
            data:{
                datasets:NewDate
            }
        }));

    }

    render(){
        return this.state.data.datasets[0].data.length
            ? <Bar style={{maxWidth:1000}} data={this.state.data} options={this.state.options} /> 
        : null;
        
    }
}*/
//#endregion

const BarGraph = () => {
    const [userInfo,setUserInfo] = useState();
    const chart=()=>{
       /* let label = ["1","2","3"];
        let dayUse =[];

        axios
            .get('https://localhost:44337/api/Retention/LifeSpan')
            .then(res=>{
                console.log(res);
                dayUse = res.data;
            
            setUserInfo({
                labels:"user info",
                datasets:[
                    {
                        label: "level of thiccness",
                        data: dayUse,
                        backgroundColor: ["rgba(75, 192, 192, 0.6)"],
                        borderWidth: 4
                    }
                ]
            });

        });
*/      

        let label = ["1","2","3"];
        let dayUse =[];
        axios
          .get('/api/Retention/LifeSpan')
          .then(res => {
            console.log(res);
            dayUse = res.data
            dayUse.sort();
            setUserInfo({
              labels: "user info",
              datasets: [
                {
                  label: "level of thiccness",
                  data: dayUse,
                  backgroundColor: ["rgba(75, 192, 192, 0.6)"],
                  borderWidth: 4
                }
              ]
            });
          })
          .catch(err => {
            console.log(err);
          });
        console.log(label, dayUse);        
    };
    useEffect(() => {
        chart();
      }, []);
    return(
        <Line
            data={userInfo}
            options={{
                responsive: true,
                title: { text: "THICCNESS SCALE", display: true },
                scales: {
                  yAxes: [
                    {
                      ticks: {
                        autoSkip: true,
                        maxTicksLimit: 10,
                        beginAtZero: true
                      },
                      gridLines: {
                        display: false
                      }
                    }
                  ],
                  xAxes: [
                    {
                      gridLines: {
                        display: false
                      }
                    }
                  ]
                }
              }}
        />    
    )
}

/*const Graph=()=>{
    
    
        let a = axios.get('https://localhost:44337/api/Retention/LifeSpan')
        console.log(a)
    

    /* let NewDate = this.state.data.datasets[0];
        let arr =[];
        await axios.get('https://localhost:44337/api/Retention/LifeSpan')
            .then(responce=>{
                NewDate.data = responce.data;
                console.log(this.state);
            }); 
}
*/
export default BarGraph;
