import {useDispatch} from "react-redux";
import React, {FC, useCallback, useState} from "react";
import {RACIMatrixDto, RACIStatus, raciStatusToName, raciValues} from "../../../store/types";
import Button from "../../../components/Button/Button";
import Icon from "../../../components/Icon/Icon";
import styled from "styled-components";
import {RACIDownload} from "../../../store/actions";

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

  border: 2px solid rgb(92, 173, 99);

  min-width: 40%;
  max-width: 80%;
  font-family: Arial, Helvetica, sans-serif;
  border-collapse: collapse;

  overflow: scroll;
`;

export const THWhite = styled.th`
  text-align: center;
  
  background-color: #fff;
  border: 2px solid rgba(92, 173, 99);

  padding: 12px;
`;

export const TDWhite = styled.td`
  padding: 12px;

  background-color: #fff;
  border: 2px solid rgba(92, 173, 99);
`;

export const TDWhiteSmallPadding = styled.td`
  padding: 6px;

  background-color: #fff;
  border: 2px solid rgba(92, 173, 99);
`;

export const TR = styled.tr`

  height: 10px;
`;

const mapRaciToColor = (raci: RACIStatus) => {
    switch(raci){
        case RACIStatus.Responsible:
            return "rgba(147, 188, 152, 0.34)"
        case RACIStatus.Accountable:
            return "rgba(58, 165, 68, 0.34)";
        case RACIStatus.Consulted:
            return "rgba(51, 87, 54, 0.34)";
        case RACIStatus.Informed:
            return "rgba(180, 180, 180, 0.34)";

        default:
            return "#fff"
    }
}

const RACIMatrixPage = () => {
    const dispatch = useDispatch();

    const [executors, setExecutors] = useState<string[]>([]);
    const [tasks, setTasks] = useState<string[]>([]);

    const [matrix, setMatrix] = useState<RACIStatus[][]>([]);

    const onChangeExecutor = (ind: number, v: string) => {
        let items = executors.slice();
        items[ind] = v;

        setExecutors(items);
    }

    const onChangeTask = (ind: number, v: string) => {
        let items = tasks.slice();
        items[ind] = v;

        setTasks(items);
    }

    const addEmptyColumn = () => {
        let exItems = executors.slice();
        exItems.push("-")

        let newMatrix: RACIStatus[][] = [];
        matrix.forEach(taskAr => {
            let newAr = taskAr.slice();
            newAr.push(RACIStatus.Unknown);

            newMatrix.push(newAr)
        })

        setExecutors(exItems);
        setMatrix(newMatrix);
    }

    const addEmptyRow = () => {
        let taskItems = tasks.slice();
        taskItems.push("-")

        const emptyRow: RACIStatus[] = Array(executors.length).fill(RACIStatus.Unknown);
        let newMatrix = matrix.slice();
        newMatrix.push(emptyRow);

        setTasks(taskItems);
        setMatrix(newMatrix);
    }

    const onRaciSelected = (taskInd: number, rInd: number, rStr: string) => {
        let newMatrix = matrix.slice();

        newMatrix[taskInd][rInd] = parseInt(rStr);

        setMatrix(newMatrix);
    }

    const onRaciDownload = useCallback(() => {
        const value: RACIMatrixDto = {
            executors: executors,
            tasks: tasks,
            RACI: matrix
        }

        console.log('RACI download: ' + JSON.stringify(value));

        dispatch<RACIDownload>({
            type: 'RACI_DOWNLOAD',
            payload: value
        });
    }, [dispatch, executors, tasks, matrix]);

    return (
        <AlignCenter>
            <h1 style={{marginTop: "2rem"}}>RACI матриця</h1>
            <NarrowTable>
                <thead>
                <TR>
                    <THWhite></THWhite>
                    {executors.map((e, ind) =>
                        <THWhite>
                            <TextInput
                                type={"text"}
                                value={e}
                                onChange={(e) => {
                                    onChangeExecutor(ind, e.target.value)
                                }}
                            />
                        </THWhite>
                    )}
                    <THWhite>
                        <Button buttonType='button' styleType='none' onClick={addEmptyColumn}>
                            <Icon type='green-plus' style={{width: 30, height: 30 }} />
                        </Button>
                    </THWhite>
                </TR>
                </thead>
                <tbody>
                {tasks.map((t, ind) =>
                    <TR key={ind}>
                        <TDWhite>
                            <TextInput
                                type={"text"}
                                value={t}
                                onChange={(e) => {
                                    onChangeTask(ind, e.target.value)
                                }}
                            />
                        </TDWhite>
                        {matrix[ind].map((raci, rInd) =>
                            <TDWhiteSmallPadding key={`${ind}_${rInd}`}>
                                <select style={{
                                    backgroundColor: mapRaciToColor(raci),
                                    width: "100%",
                                    height: "75px",
                                    border: 0,
                                    outline: 0,
                                    fontWeight: "bold",
                                    fontSize: "large",
                                    textAlign: "center"
                                }} onChange={e => {
                                    onRaciSelected(ind, rInd, e.target.value)
                                }}>
                                    {raciValues.map(opt =>
                                        <option
                                            selected={raci === opt}
                                            value={opt}
                                            label={raciStatusToName(opt)}
                                        ><p>{raciStatusToName(opt)}</p></option>
                                    )}
                                </select>
                            </TDWhiteSmallPadding>
                        )}
                        <THWhite></THWhite>
                    </TR>)}
                <TR>
                    <TDWhite>
                        <AlignCenter>
                            <Button buttonType='button' styleType='none' onClick={addEmptyRow}>
                                <Icon type='green-plus' style={{width: 30, height: 30 }} />
                            </Button>
                        </AlignCenter>
                    </TDWhite>
                    {executors.map(_ =>
                    <THWhite></THWhite>)}
                </TR>
                </tbody>
            </NarrowTable>

            <Button buttonType='button' styleType='simple' onClick={() => {
                onRaciDownload()
            }}>
                Скачати
            </Button>
        </AlignCenter>
    );
}

export default RACIMatrixPage;