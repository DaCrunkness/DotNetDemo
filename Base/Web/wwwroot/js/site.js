// Getting device and os information

//function detectDeviceType() {
//    const userAgent = navigator.userAgent.toLowerCase();
//    if (/mobile/i.test(userAgent)) {
//        return "Mobile";
//    } else if (/tablet|ipad|playbook|silk/i.test(userAgent)) {
//        return "Tablet";
//    } else {
//        return "Desktop";
//    }
//}

//console.log(detectDeviceType());

//function detectOS() {
//    const userAgent = navigator.userAgent.toLowerCase();
//    if (userAgent.indexOf("win") !== -1) {
//        return "Windows";
//    } else if (userAgent.indexOf("mac") !== -1) {
//        return "MacOS";
//    } else if (userAgent.indexOf("linux") !== -1) {
//        return "Linux";
//    } else if (userAgent.indexOf("android") !== -1) {
//        return "Android";
//    } else if (userAgent.indexOf("iphone") !== -1 || userAgent.indexOf("ipad") !== -1) {
//        return "iOS";
//    } else {
//        return "Unknown OS";
//    }
//}

//console.log(detectOS());

//function detectDeviceAndOS() {
//    const userAgent = navigator.userAgent.toLowerCase();
//    let deviceType = "Desktop";
//    let os = "Unknown OS";

//    if (/mobile/i.test(userAgent)) {
//        deviceType = "Mobile";
//    } else if (/tablet|ipad|playbook|silk/i.test(userAgent)) {
//        deviceType = "Tablet";
//    }

//    if (userAgent.indexOf("win") !== -1) {
//        os = "Windows";
//    } else if (userAgent.indexOf("mac") !== -1) {
//        os = "MacOS";
//    } else if (userAgent.indexOf("linux") !== -1) {
//        os = "Linux";
//    } else if (userAgent.indexOf("android") !== -1) {
//        os = "Android";
//    } else if (userAgent.indexOf("iphone") !== -1 || userAgent.indexOf("ipad") !== -1) {
//        os = "iOS";
//    }

//    return { deviceType, os };
//}

//const { deviceType, os } = detectDeviceAndOS();
//console.log(`Device Type: ${deviceType}`);
//console.log(`Operating System: ${os}`);



