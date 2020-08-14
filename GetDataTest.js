// JavaScript GetDataTest
var TestWindow

function OpenTestWindow(TestOn){
	if(TestOn==1){
		TestWindow = window.open('TestWindow.html');	
	}
}

function CreateObject(){
	
	var Campaign0 = new Object()
	Campaign0.id = 1
	Campaign0.name = 'AMEX'
	
	var Campaign1 = new Object()
	Campaign1.id = 2
	Campaign1.name = 'BANAMEX'	
	
	var Campaign2 = new Object()
	Campaign2.id = 3
	Campaign2.name = 'BANCOMER'	
	
	var Campaignsss = new Array (3) 
	Campaignsss[0] = Campaign0
	Campaignsss[1] = Campaign1
	Campaignsss[2] = Campaign2
	
	onCampaigns(Campaignsss)
}


function GetDataTest(code, Array1, Array2){
	TestWindow.document.write("<table width=200 border=1 cellpadding=1 cellspacing=1>"); 
	for (i=0;i<Array1.length;i++){ 
   	TestWindow.document.write("<tr>") 
		 switch (code)
  		{
			 case 1://onUnavailableTypes
					TestWindow.document.write("<td><b>UnavailableType " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].statusName + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].nAvailable + "</td>") 
			 break;	
			 
			 case 2: //onUnavailableHistory
			 		TestWindow.document.write("<td><b>UnavailableHistory " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].count + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].total + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].accumulatedTime + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].maxTime + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].statusName + "</td>")
					TestWindow.document.write("<td>")
						TestWindow.document.write("<table width=200 border=1 cellpadding=1 cellspacing=1>"); 
						TestWindow.document.write("<tr>") 
						Array2 = Array1[i].statusLog
						if(Array2 != null){
							for (j=0;j<Array2.length;j++){
							TestWindow.document.write("<td><b>UnavailableStatusHistory.statusLog " + j + "</b></td>") 
							TestWindow.document.write("<td>" + Array2[j].time + "</td>") 
							TestWindow.document.write("<td>" + Array2[j].hour + "</td>")
							}
						}
						TestWindow.document.write("</tr>") 
						TestWindow.document.write("</table>")
					TestWindow.document.write("<td>") 
			 break;	
			 
			 case 3://onCallHistory
					TestWindow.document.write("<td><b>CallHistory " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].type + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].hour + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].telephone + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].campaignName + "</td>")
					TestWindow.document.write("<td>" + Array1[i].duration + "</td>") 
			 break;
			 
			 case 4: //onAdministrators
					TestWindow.document.write("<td><b>Administrator " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i] + "</td>")
			 break;		
			 
			 case 5://Campaigns
					TestWindow.document.write("<td><b>Campaign " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].name + "</td>") 	 
			 break;	
			 
			 case 6://onTransferOptions Agent
			 		TestWindow.document.write("<td><b>transfersOptions_agent " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].name + "</td>") 
			 break;	
			 
			 case 7://onTransferOptions Phones  
					TestWindow.document.write("<td><b>transfersOptions_phone " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].name + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].phone + "</td>") 
			 break;
			 
			 case 8://onTransferOptions acds
			 		TestWindow.document.write("<td><b>transfersOptions_acd " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].name + "</td>") 
					
			 break;
			 
			 case 9://onDispositions dispositions
			 		TestWindow.document.write("<td><b>Dispositions_disposition " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i].id + "</td>") 
					TestWindow.document.write("<td>" + Array1[i].name + "</td>") 
			 break; phoneNumbers
			 
			 case 10://onDispositions phoneNumbers
			 		TestWindow.document.write("<td><b>Dispositions_phoneNumber " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i] + "</td>") 
			 break;
			 
			 case 12://ContactData
			 		TestWindow.document.write("<td><b>ContactData " + i + "</b></td>") 
					TestWindow.document.write("<td>" + Array1[i] + "</td>") 
			 break;
			 
			 case 11://onReprogramCall
			 		TestWindow.document.write("<td><b>onReprogramCall_callBacks - </b></td>") 
					TestWindow.document.write("<td>" + Array1[i].date + "</td>")  
					TestWindow.document.write("<td>")
						TestWindow.document.write("<table width=200 border=1 cellpadding=1 cellspacing=1>"); 
						TestWindow.document.write("<tr>") 
						Array2 = Array1[i].callBacks
						if(Array2 != null){
							for (j=0;j<Array2.length;j++){
							TestWindow.document.write("<td><b>onReprogramCall_callBacks_callBacks " + j + "</b></td>") 
							TestWindow.document.write("<td>" + Array2[j].hour + "</td>") 
							TestWindow.document.write("<td>" + Array2[j].nCallBacks + "</td>")
							}
						}
						TestWindow.document.write("</tr>") 
						TestWindow.document.write("</table>")
					TestWindow.document.write("<td>") 
					
		}
   	TestWindow.document.write("</tr>") 
	} 
	
	TestWindow.document.write("</table>")
}