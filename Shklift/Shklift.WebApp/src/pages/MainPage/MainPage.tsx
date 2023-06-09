import { SubmitButton } from "../../components/Button/SubmitButton"
import { Card } from "../../components/Card/Card"
import { useCardInput } from "./UseCardInput"

interface MainPageProps {
}

export const MainPage = ({ }: MainPageProps) => {

    const useCardForm = useCardInput();

    return (
        <>
            <section className="h-4/5">
                <div
                    className="h-full"
                >
                    <form onSubmit={useCardForm.handleInputInfo} className="bg-gray-100">
                        <div className="flex justify-center pt-24 flex-wrap">
                            <Card 
                                isSender={true} 
                                title="Відправник" 
                                cardNumberInputHook={useCardForm.cardSenderNumber} 
                                cvvInputHook={useCardForm.cvvInput}
                                dateInputHook={[useCardForm.dateSender.cardDate.month, useCardForm.dateSender.cardDate.year]}
                                moneyInputHook={useCardForm.moneyInput}
                            />
                            <Card 
                                isSender={false} 
                                title="Отримувач" 
                                cardNumberInputHook={useCardForm.cardReceiverNumber}
                            />
                        </div>
                        <SubmitButton />
                    </form>
                </div>
            </section>
        </>
    )
}