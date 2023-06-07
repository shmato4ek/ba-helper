import React, {useCallback, useState} from "react";
import styled from "styled-components";
import {CommunicationPlanDto, DownloadCommunicationPlan} from "../../../store/types";
import Button from "../../../components/Button/Button";
import Icon from "../../../components/Icon/Icon";
import {PlanDownload} from "../../../store/actions";
import {useDispatch} from "react-redux";

export const TextInput = styled.input`
  font-family: Arial, Helvetica, sans-serif;
  font-size: medium;
  border: 0;
  outline: 0;
`;

export const AlignCenter = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

export const NarrowTable = styled.table`
  margin: 2rem;

  border: 2px solid rgba(126, 211, 135, 0.51);

  width: 60%;
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  overflow: scroll;
`;

export const THGreen = styled.th`
  text-align: center;
  background-color: rgba(126, 211, 135, 0.51);

  padding: 12px;
`;

export const TDWhite = styled.td`
  padding: 12px;

  background-color: #fff;
  border: 2px solid rgba(126, 211, 135, 0.51);
`;

export const TDWhiteWithoutBorder = styled.td`
  padding: 12px;

  background-color: #fff;
`;

export const TR = styled.tr`

  height: 10px;
`;

const CommunicationPlanPage = () => {
    const dispatch = useDispatch();

    const [planState, setPlanState] = useState<CommunicationPlanDto[]>([])

    const addEmptyRow = () => {
        let items = planState.slice()
        items.push({
            audience: "-", channel: "-", description: "-", frequency: "-", organizer: "-"
        })

        setPlanState(items)
    }

    const replaceRow = (ind: number, item: CommunicationPlanDto) => {
        let items = planState.slice();
        items[ind] = item

        setPlanState(items)
    }

    const onPlanDownload = useCallback(() => {
        console.log(JSON.stringify(planState));
        const value: DownloadCommunicationPlan = {
            plan: planState
        }

        console.log('Table download: ' + JSON.stringify(value));

        dispatch<PlanDownload>({
            type: 'PLAN_DOWNLOAD',
            payload: value
        });
    }, [dispatch, planState]);


    /*
  const onSubmit = useCallback(
    (values: PostDocumentDto) => {
      const postDocumentDto: PostDocumentDto = {
        name: values.name,
        projectAim: values.projectAim,
        glossaries: glossaries,
        userStories: userStories,
      };

      console.log("Create Project Page values submit");
      console.log(JSON.stringify(postDocumentDto, null, 2));

      dispatch<PostDocumentAction>({
        type: "POST_DOCUMENT",
        payload: postDocumentDto,
        navigate,
      });
    },
    [dispatch, navigate, glossaries, userStories]
  );
    */
    return (
        <AlignCenter>
            <h1 style={{marginTop: "2rem"}}>План комунікації</h1>
            <NarrowTable>
                <thead>
                <TR>
                    <THGreen>Опис</THGreen>
                    <THGreen>Частота</THGreen>
                    <THGreen>Канал</THGreen>
                    <THGreen>Авдиторія</THGreen>
                    <THGreen>Організація</THGreen>
                </TR>
                </thead>
                <tbody>
                {planState.map((v, ind) =>
                    <TR key={ind}>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={v.description}
                                onChange={(e) => {
                                    v.description = e.target.value

                                    replaceRow(ind, v)
                                }}
                            />
                        </TDWhite>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={v.frequency}
                                onChange={(e) => {
                                    v.frequency = e.target.value

                                    replaceRow(ind, v)
                                }}
                            />
                        </TDWhite>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={v.channel}
                                onChange={(e) => {
                                    v.channel = e.target.value

                                    replaceRow(ind, v)
                                }}
                            />
                        </TDWhite>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={v.audience}
                                onChange={(e) => {
                                    v.audience = e.target.value

                                    replaceRow(ind, v)
                                }}
                            />
                        </TDWhite>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={v.organizer}
                                onChange={(e) => {
                                    v.organizer = e.target.value

                                    replaceRow(ind, v)
                                }}
                            />
                        </TDWhite>
                    </TR>)}
                <TR>
                    <TDWhiteWithoutBorder>
                        <Button buttonType='button' styleType='none' onClick={addEmptyRow}>
                            <Icon type='green-plus' style={{width: 30, height: 30 }} />
                        </Button>
                    </TDWhiteWithoutBorder>
                    <TDWhiteWithoutBorder></TDWhiteWithoutBorder>
                    <TDWhiteWithoutBorder></TDWhiteWithoutBorder>
                    <TDWhiteWithoutBorder></TDWhiteWithoutBorder>
                    <TDWhiteWithoutBorder></TDWhiteWithoutBorder>
                </TR>
                </tbody>
            </NarrowTable>

            <Button buttonType='button' styleType='simple' onClick={() => {
                onPlanDownload()
            }}>
                Скачати
            </Button>
        </AlignCenter>
    );
}

export default CommunicationPlanPage;