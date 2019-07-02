import React, { Component } from 'react'
import {
  Text,
  View,
} from 'react-native'
import styles from './styles'

export default class SiblingZOrder extends Component {

  render() {
    return (
      <View style={styles.content}>
        <Text style={styles.subCaption}>Check issue https://github.com/facebook/react-native/issues/18344</Text>
        <Text>zIndex only works when Views are siblings of each other</Text>
        <View style={styles.testBar}>
          <Text>Sibling no Z-Order</Text>
          <View>
            <View styles={styles.item}>
              <View style={styles.redBox} />
              <View style={styles.greenBox} />
            </View>
          </View>
          <Text>    Sibling with Z-Order</Text>
          <View styles={styles.item}>
            <View style={[styles.redBox, { zIndex: 10 }]} />
            <View style={styles.greenBox} />
          </View>
          <Text>   NonSibling with Z-Order</Text>
          <View styles={styles.item}>
            <View style={[styles.redBox, { zIndex: 10 }]} />
          </View>
        </View>
        <View id='box for NonSibling with Z-Order' style={[styles.greenBox, { top: 80, right: 460 }]} />
      </View>
    )
  }
}
