// JavaScript CORE API Methods

//username:String, password:String 
function login(username, password){
	document.getElementById("myTextArea").value += 'Begin - login: ' + username + '-' + password +'\n'
	JavaScriptPackageRemote.login(username, password)
	document.getElementById("myTextArea").value += 'End - login' +'\n'
}

//id:uint
function SetOnUnavailableStatus(id){
	//alert('SetOnUnavailableStatus: ' + id)
	//Condicion para saber que mando la caja de texto llamada "ponerUnavailable"
	//Condicion para que muestre la informacion correcta de acuerdo alo seleccionado en la caja de texto
	//SI NO contiene nada el combobox que muestre un mensaje
	if(id == "" && document.getElementById("UnavailableType").value != ""){
		id = 1;
	}else if (id == "" && document.getElementById("UnavailableType").value == ""){
		document.getElementById("myTextArea").value += 'Begin - SetOnUnavailableStatus: ' + 'El combo no puede estar vacio'+'\n'
	}
	
	if (id == 1){
		document.getElementById("myTextArea").value += 'Begin - SetOnUnavailableStatus: ' + id + 'Comida'+'\n'
		JavaScriptPackageRemote.SetOnUnavailableStatus(id)
		document.getElementById("myTextArea").value += 'End - SetOnUnavailableStatus' +'\n'
	}else if(id == 2){
		document.getElementById("myTextArea").value += 'Begin - SetOnUnavailableStatus: ' + id + 'Descanso' +'\n'
		JavaScriptPackageRemote.SetOnUnavailableStatus(id)
		document.getElementById("myTextArea").value += 'End - SetOnUnavailableStatus' +'\n'
	}else if (id == 3){
		document.getElementById("myTextArea").value += 'Begin - SetOnUnavailableStatus: ' + id + 'Desayuno' +'\n'
		JavaScriptPackageRemote.SetOnUnavailableStatus(id)
		document.getElementById("myTextArea").value += 'End - SetOnUnavailableStatus' +'\n'
	}
	
}
				
function SetReady(){
		//alert('SetReady')
		document.getElementById("myTextArea").value += 'Begin - SetReady' +'\n'
		JavaScriptPackageRemote.SetReady()	
		document.getElementById("myTextArea").value += 'End - SetReady' +'\n'
		
		//Pongo en el cuadro de texto llamado "ponerSetReady"
		document.getElementById("ponerSetReady").value = 'Begin - End SetReady';
}
	
//Id:uint
function transferCallToAgent(Id){		
	//alert('transferCallToAgent: ' + Id)
	document.getElementById("myTextArea").value += 'Begin - transferCallToAgent: ' + Id +'\n'
	JavaScriptPackageRemote.transferCallToAgent(Id)		
	document.getElementById("myTextArea").value += 'End - transferCallToAgent' +'\n'
}	

//Id:uint
function trasnferCallToACD(Id){
	//alert('trasnferCallToACD: ' + Id)
	document.getElementById("myTextArea").value += 'Begin - trasnferCallToACD: ' + Id +'\n'
	JavaScriptPackageRemote.trasnferCallToACD(Id)
	document.getElementById("myTextArea").value += 'End - trasnferCallToACD' +'\n'
}

function assistedDialNumber(Phone){	
	//alert('assistedDialNumber' + Phone)
	document.getElementById("myTextArea").value += 'Begin - assistedDialNumber: ' + Phone +'\n'
	JavaScriptPackageRemote.assistedDialNumber(Phone)
	document.getElementById("myTextArea").value += 'End - assistedDialNumber' +'\n'
}
function assistedXFerHangUP(){	
	//alert('assistedXFerHangUP')
	document.getElementById("myTextArea").value += 'Begin - assistedXFerHangUP: ' + '\n'
	JavaScriptPackageRemote.assistedXFerHangUP()
	document.getElementById("myTextArea").value += 'End - assistedXFerHangUP' +'\n'
}
function assistedXFerTransferCalls(){		
	document.getElementById("myTextArea").value += 'Begin - assistedXFerTransferCalls: ' +'\n'
	JavaScriptPackageRemote.assistedXFerTransferCalls()
	document.getElementById("myTextArea").value += 'End - assistedXFerTransferCalls' +'\n'
}
function assistedXFerUseMainCall(){		
	//alert('assistedXFerTransferCalls')
	document.getElementById("myTextArea").value += 'Begin - assistedXFerUseMainCall: ' +'\n'
	JavaScriptPackageRemote.assistedXFerUseMainCall()
	document.getElementById("myTextArea").value += 'End - assistedXFerUseMainCall' +'\n'
}
function assistedXFerUseSecondCall(){
	//alert('assistedDialNumber')
	document.getElementById("myTextArea").value += 'Begin - assistedXFerUseSecondCall: '+'\n'
	JavaScriptPackageRemote.assistedXFerUseSecondCall()
	document.getElementById("myTextArea").value += 'End - assistedXFerUseSecondCall' +'\n'
}	

//Number:String
function transferCallToPhoneNumber(Number){
	//alert('transferCallToPhoneNumber: ' + Number)
	document.getElementById("myTextArea").value += 'Begin - transferCallToPhoneNumber: ' + Number +'\n'
	JavaScriptPackageRemote.transferCallToPhoneNumber(Number)	
	document.getElementById("myTextArea").value += 'End - transferCallToPhoneNumber' +'\n'
}

//callOutID:int, CallID:int, phoneNumber:String, campID:uint
//phoneNum:String, camID:Number, clientName:String
function makeManualCall(phoneNum,campID,clientName){
	//alert('makeManualCall: ' + callOutID + ' - ' + CallID + ' - ' + phoneNumber + ' - ' + campID)
	document.getElementById("myTextArea").value += 'Begin - makeManualCall: ' + phoneNum + ' - ' + campID + ' - ' + clientName +'\n'
	//JavaScriptPackageRemote.makeManualCall(callOutID, CallID, phoneNumber, campID)
	JavaScriptPackageRemote.makeManualCall(phoneNum, campID, clientName)
	document.getElementById("myTextArea").value += 'End - makeManualCall' +'\n'
}

function hangUpManualDial(){
	document.getElementById("myTextArea").value += 'Begin - hangUpManualDial\n';
	JavaScriptPackageRemote.hangUpManualDial();
	document.getElementById("myTextArea").value += 'End - hangUpManualDial\n';
}		

//aToAdministrator:String, aMessage:String
function SendChatMessage(Administrator, Message){
	//alert('SendChatMessage: ' + Administrator + ' - ' + Message)
	document.getElementById("myTextArea").value += 'Begin - SendChatMessage: ' + Administrator + ' - ' + Message +'\n'
	JavaScriptPackageRemote.SendChatMessage(Administrator, Message)
	document.getElementById("myTextArea").value += 'End - SendChatMessage' +'\n'
}

function AVRS_Start(){
	//alert('AVRS_Start')
	document.getElementById("myTextArea").value += 'Begin - AVRS_Start' +'\n'
	JavaScriptPackageRemote.AVRS_Start()	
	document.getElementById("myTextArea").value += 'End - AVRS_Start' +'\n'
	
	//Mando a la Caja de texto llamada "avrsComenzo" un texto
	document.getElementById("avrsComenzo").value = 'Avrs - Comenzo';
}

function AVRS_Stop(){
	//alert('AVRS_Stop')
	document.getElementById("myTextArea").value += 'Begin - AVRS_Stop' +'\n'
	JavaScriptPackageRemote.AVRS_Stop()		
	document.getElementById("myTextArea").value += 'End - AVRS_Stop' +'\n'
	
	//Mando a la Caja de texto llamada "avrsParo" un texto
	document.getElementById("avrsParo").value = 'Avrs - Detenido';
}

function AVRS_Delete(){
	//alert('AVRS_Delete')
	document.getElementById("myTextArea").value += 'Begin - AVRS_Delete' +'\n'
	JavaScriptPackageRemote.AVRS_Delete()		
	document.getElementById("myTextArea").value += 'End - AVRS_Delete' +'\n'
	
	//Mando a la Caja de texto llamada "avrsEliminar" un texto
	document.getElementById("avrsEliminar").value = 'Avrs - Eliminado';
}	

function GetLastCallData(){ //CenterwareCallData
	//alert('GetLastCallData')
	document.getElementById("myTextArea").value += 'Begin - GetLastCallData' +'\n'
	var arrayx = JavaScriptPackageRemote.GetLastCallData()
	
	document.getElementById("myTextArea").value += 'End - GetLastCallData - Out: call_id ' + arrayx.call_id + '- cam_id ' + arrayx.cam_id + '- cal_key ' + arrayx.cal_key + '- callOut_id ' + arrayx.callOut_id + '- typeCall ' + arrayx.typeCall + '- IsQueueCall ' + arrayx.IsQueueCall + '- holdTime ' + arrayx.holdTime + '- phoneNumber ' + arrayx.phoneNumber + '- port ' + arrayx.port + '- wrapUpTime ' + arrayx.wrapUpTime + '- contactData ' + arrayx.contactData[0]+ ',' + arrayx.contactData[1] + ',' + arrayx.contactData[2] + ',' + arrayx.contactData[3] + ',' + arrayx.contactData[4] + '\n'
	
}	

function getAgentID(){ //uint
	//alert('getAgentID')
	document.getElementById("myTextArea").value += 'Begin - getAgentID' +'\n'
	//JavaScriptPackageRemote.getAgentID()
	document.getElementById("myTextArea").value += 'End - getAgentID - Out: ' + JavaScriptPackageRemote.getAgentID() +'\n'
	
	//Pongo en la Caja de Texto Llamada "ponerGetAgentId" 
	document.getElementById("ponerGetAgentId").value='Out: ' + JavaScriptPackageRemote.getAgentID();
}

function getUsername(){ //String
	//alert('getUsername')
	document.getElementById("myTextArea").value += 'Begin - getUsername' +'\n'
	//JavaScriptPackageRemote.getUsername()
	document.getElementById("myTextArea").value += 'End - getUsername - Out: ' + JavaScriptPackageRemote.getUsername() +'\n'
	
	//Pongo en la Caja de Texto Llamada "ponerGetUserName" el valor que trae la funcion JavaScriptPackageRemote.getUserName()
	document.getElementById("ponerGetUserName").value = 'getUserName: ' + JavaScriptPackageRemote.getUsername();
}

function getExtenstion(){ //String
	//alert('getExtenstion')
	document.getElementById("myTextArea").value += 'Begin - getExtenstion' +'\n'
	//JavaScriptPackageRemote.getExtenstion()
	document.getElementById("myTextArea").value += 'End - getExtenstion - Out: ' + JavaScriptPackageRemote.getExtenstion() +'\n'
	
	//Pongo en la caja de Texto Llamada "ponerGetExtension" el valor que trae la funcion JavaScriptPackageRemote.getExtenstion()
	document.getElementById("ponerGetExtension").value = 'getExtension: ' + JavaScriptPackageRemote.getExtenstion();
}
		
function holdCall(){
	//alert('holdCall')
	document.getElementById("myTextArea").value += 'Begin - holdCall' +'\n'
	JavaScriptPackageRemote.holdCall()
	document.getElementById("myTextArea").value += 'End - holdCall' +'\n'
	
	//Pongo en la caja de Texto Llamada "ponerHoldCall" el valor que trae la funcion JavaScriptPackageRemote.holdCall()
	document.getElementById("ponerHoldCall").value = 'Hold Call: ' + JavaScriptPackageRemote.holdCall();
}

function setMute(enable){
	document.getElementById("myTextArea").value += 'Begin - setMute: ' + enable +'\n';
	JavaScriptPackageRemote.setMute(enable);
	document.getElementById("myTextArea").value += 'End - setMute\n';
}

//aDigit:String
function DTMFDigit(aDigit){
	//alert('DTMFDigit: ' + aDigit)
	document.getElementById("myTextArea").value += 'Begin - DTMFDigit: ' + aDigit +'\n'
	JavaScriptPackageRemote.DTMFDigit(aDigit)
	document.getElementById("myTextArea").value += 'End - DTMFDigit: ' +'\n'
	
	//Pongo en la caja de Texto Llamada "numeros" el numero que se tecleo
	document.getElementById("numeros").value = aDigit;
}	

function hangUpCall(){
	//alert('hangUpCall')
	document.getElementById("myTextArea").value += 'Begin - hangUpCall' +'\n'
	JavaScriptPackageRemote.hangUpCall()
	document.getElementById("myTextArea").value += 'End - hangUpCall' +'\n'
	
	//Pongo en la caja de Texto Llamada "ponerHangUpCall" un texto de que se ejecuto el hangUpCall
	document.getElementById("ponerHangUpCall").value = 'Se Ejecuto hangUpCall';
}


function CloseSession(){
	//alert('CloseSession')
	document.getElementById("myTextArea").value += 'Begin - CloseSession' +'\n'
	JavaScriptPackageRemote.CloseSession()
	document.getElementById("myTextArea").value += 'End - CloseSession' +'\n'
	
	//Poner en la Caja de Texto Llamada "ponerCloseSession"
	document.getElementById("ponerCloseSession").value = 'Cerro Sesion';
}

/* **********************************************************************
			REQUESTS 
********************************************************************** */


function getCallHistory(){
	//alert('CallHistory')
	document.getElementById("myTextArea").value += 'Begin - CallHistory' +'\n'
	JavaScriptPackageRemote.getCallHistory()
	document.getElementById("myTextArea").value += 'End - CallHistory' +'\n'
	
	//Pongo en la Caja de Texto Llamada "ponerGetCallHistory" 
	document.getElementById("ponerGetCallHistory").value ='Comenzo Call History';
}

function getUnavailables(){
	//alert('Unavailables')
	document.getElementById("myTextArea").value += 'Begin - Unavailables' +'\n'
	JavaScriptPackageRemote.getUnavailables()
	document.getElementById("myTextArea").value += 'End - Unavailables' +'\n'
}

function getunavalibaleHistory(){
	//alert('UnavailableHistory')
	document.getElementById("myTextArea").value += 'Begin - UnavailableHistory' +'\n'
	JavaScriptPackageRemote.getunavalibaleHistory()
	document.getElementById("myTextArea").value += 'End - UnavailableHistory' +'\n'
	
	//Poner En la Caja de Texto Llamada "ponerUnavailableHistory" 
	document.getElementById("ponerUnavailableHistory").value = 'Comenzo UnavailableHistory';
}

//outboundType:Boolean, idACD:uint = 0
function getTransfersOptions(outboundType, idACD){
	//alert('TransfersOptions: ' + outboundType + ' - ' + idACD)
	//alert(outboundType)
	document.getElementById("myTextArea").value += 'Begin - TransfersOptions: ' + outboundType + ' - ' + idACD +'\n'
	JavaScriptPackageRemote.getTransfersOptions(outboundType, idACD)
	document.getElementById("myTextArea").value += 'End - TransfersOptions' +'\n'
}

function getSupervisorsToChat(){
	//alert('SupervisorsToChat')
	document.getElementById("myTextArea").value += 'Begin - SupervisorsToChat' +'\n'
	JavaScriptPackageRemote.getSupervisorsToChat()
	document.getElementById("myTextArea").value += 'End - SupervisorsToChat' +'\n'
	
	//Pongo en la Caja de Texto Llamada "getAdministrator"
	document.getElementById("ponerGetAdministrator").value = 'Comenzo getSupervisorsToChat';
}

function getCampaignsRelated(){
	//alert('CampaignsRelated')
	document.getElementById("myTextArea").value += 'Begin - CampaignsRelated' +'\n'
	JavaScriptPackageRemote.getCampaignsRelated()
	document.getElementById("myTextArea").value += 'End - CampaignsRelated' +'\n'
	
	//Pongo en la Caja de texto Llamada "ponerGetCampaign"
	document.getElementById("ponerGetCampaign").value = 'Ver Campañas';
}

//idACD:uint
function getACDDispositions(idACD){
	//alert('ACDDispositions: ' + idACD)
	document.getElementById("myTextArea").value += 'Begin - ACDDispositions: ' + idACD +'\n'
	JavaScriptPackageRemote.getACDDispositions(idACD)
	document.getElementById("myTextArea").value += 'End - ACDDispositions' +'\n'
}

//idDisposition:uint, callID:uint
function disposeACDCall(idDisposition, callID){
	document.getElementById("myTextArea").value += 'Begin - disposeACDCall: ' + idDisposition + ' - ' + callID +'\n'
	JavaScriptPackageRemote.disposeACDCall(idDisposition, callID)
	document.getElementById("myTextArea").value += 'End - disposeACDCall: ' +'\n'
}

//idCampaign:uint
function getCampaignDispositions(idCampaign){
	//alert('CampaignDispositions: ' + idCampaign)
	document.getElementById("myTextArea").value += 'Begin - CampaignDispositions: ' + idCampaign +'\n'
	JavaScriptPackageRemote.getCampaignDispositions(idCampaign)
	document.getElementById("myTextArea").value += 'End - CampaignDispositions' +'\n'
	
}

//idCampaign:uint, callOutID:Number
function getCampaignDispositionsAndNumbers(idCampaign,callOutID){
	//alert('CampaignDispositions: ' + idCampaign + ' - ' + callOutID)
	document.getElementById("myTextArea").value += 'Begin - CampaignDispositions: ' + idCampaign + ',' + callOutID + '\n'
	JavaScriptPackageRemote.getCampaignDispositionsAndNumbers(idCampaign,callOutID)
	document.getElementById("myTextArea").value += 'End - CampaignDispositions' +'\n'
}

//callOutID:Number
function getPhoneNumbers(callOutID){
	//alert('PhonesCall: ' + callOutID)
	document.getElementById("myTextArea").value += 'Begin - PhonesCall: ' + callOutID +'\n'
	JavaScriptPackageRemote.getPhoneNumbers(callOutID)
	document.getElementById("myTextArea").value += 'End - PhonesCall' +'\n'
}

//idDiposition:uint, idCampaign:uint, callID:uint
function disposeCampaingCall(idDiposition, idCampaign, callID){
	document.getElementById("myTextArea").value += 'Begin - disposeCampaingCall: ' + idDiposition + ' - ' + idCampaign + ' - ' + callID +'\n'
	JavaScriptPackageRemote.disposeCampaingCall(idDiposition, idCampaign, callID)
	document.getElementById("myTextArea").value += 'End - disposeCampaingCall' +'\n'
}

//idCampaign:uint, idDisposition:uint, callID:uint, dateCallBack:String, callOutID:int, telephone:String, customNumber:Boolean 
function reprogramCampaignCall(idCampaign, idDisposition, callID, dateCallBack, telephone, existingNumber){
	document.getElementById("myTextArea").value += 'Begin - reprogramCampaignCall: ' + idCampaign + ' - '+ idDisposition +' - ' + callID + ' - ' + dateCallBack + ' - ' + telephone + ' - ' + existingNumber +'\n'
	JavaScriptPackageRemote.reprogramCampaignCall(idCampaign, idDisposition, callID, dateCallBack, telephone, existingNumber)
	document.getElementById("myTextArea").value += 'End - reprogramCampaignCall' +'\n'
}
		
		
/// ******** AFFECT BD ************

//callOutID:int, data1:String, data2:String, data3:String, data4:String, data5:String
function UpdateDataCall(callOutID, data1, data2, data3, data4, data5){
	//alert(callOutID)
	document.getElementById("myTextArea").value += 'Begin - UpdateDataCall: ' + callOutID + ' - ' + data1 + ' - ' + data2 + ' - ' + data3 + ' - ' + data4 + ' - ' + data5 +'\n'
	JavaScriptPackageRemote.UpdateDataCall(callOutID, data1, data2, data3, data4, data5)
	document.getElementById("myTextArea").value += 'End - UpdateDataCall' +'\n'
}

//aCurrentPassword:String, aNewPassword:String
function ChangePassword(aCurrentPassword, aNewPassword){	
	document.getElementById("myTextArea").value += 'Begin - ChangePassword: ' + aCurrentPassword + ' - ' + aNewPassword +'\n'
	JavaScriptPackageRemote.ChangePassword(aCurrentPassword, aNewPassword)
	document.getElementById("myTextArea").value += 'End - ChangePassword' +'\n'
}