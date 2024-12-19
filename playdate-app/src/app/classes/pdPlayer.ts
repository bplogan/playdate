import { pdPlayerAllergy } from "./pdPlayerAllergy";
import { pdPlayerInstruction } from "./pdPlayerInstruction";
import { pdPlayerRestriction } from "./pdPlayerRestriction";

export class pdPlayer{

    id: number = 0;
    fullName: string = "";
    emailAddress: string = "";
    userName: string = "";
    address: string = "";
    address2: string = "";
    city: string = "";
    province: string = "";
    postalCode: string = "";
    country: string = "";
    age: number = 0
    isSwimmer: boolean = false;
    isPetAllergy: boolean = false;
    isFoodRestricted: boolean = false;
    isFoodAllergy: boolean = false;
    isNutAllergy:boolean = false;
    isEggAllergy:boolean = false;
    isDairyAllergy:boolean = false;
    isOtherAllergy: boolean = false;
    isDogAllergy: boolean = false;
    isCatAllergy: boolean = false;
    isOtherRestricted: boolean = false;
    isSpecialInstructions: boolean = false;
    isVegetarian: boolean = false;
    isVegan: boolean = false;
    otherAllergy:string = "";
    otherRestricted:string = "";
    specialInstructions:string = "";
    hasEpiPen: boolean = false;
    dob: Date = new Date;
    allergies: pdPlayerAllergy[] = [];
    friends: pdPlayer[] = [];
    instructions: pdPlayerInstruction[] = [];
    restrictions: pdPlayerRestriction[] = [];

    constructor(){
        
    }
}